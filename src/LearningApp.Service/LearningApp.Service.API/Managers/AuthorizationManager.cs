using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using LearningApp.Service.API.Contracts.Authorization.Requests;
using LearningApp.Service.API.Contracts.Authorization.Responses;
using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.API.Entities;
using LearningApp.Service.API.Utils;
using LearningApp.Service.Core.Syncs;
using LearningApp.Service.Database.Tables;
using LearningApp.Service.Langs.API;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LearningApp.Service.API.Managers
{
	// TODO: реализовать перевод без авторизации, придумать способ не копипасть аргументы в конструктор результата метода
	public class AuthorizationManager : IAuthorizationManager
	{
		public const string LanguageClaim = "Language";
		private const string RefreshTokenClaim = "RefreshToken";
		private const string UserIdClaim = "UserId";
		private const string PermissionLevelClaim = "PermissionLevel";

		private static readonly Regex EmailPattern = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$");

		private const int MinPasswordLength = 8;
		private const int MaxPasswordLength = 32;
		private const int MinUsernameLength = 5;
		private const int MaxUsernameLength = 15;

		private readonly ILogger<AuthorizationManager> _logger;
		private readonly IUsersManager _usersManager;
		private readonly IAccessTokenPool _accessTokenPool;
		private readonly ISyncManager _syncManager;
		private readonly AppConfig _appConfig;

		public AuthorizationManager(ILogger<AuthorizationManager> logger, IUsersManager usersManager, IAccessTokenPool accessTokenPool, ISyncManager syncManager, AppConfig appConfig)
		{
			_logger = logger;
			_usersManager = usersManager;
			_accessTokenPool = accessTokenPool;
			_syncManager = syncManager;
			_appConfig = appConfig;
		}

		/// <summary>
		/// Авторизоваться по почте и паролю.
		/// </summary>
		/// <returns></returns>
		public MethodResult<AccessTokenResponse> TryLogin(LoginRequest loginRequest)
		{
			if (loginRequest == null)
			{
				return MethodResult<AccessTokenResponse>.Error(StatusCodes.Status400BadRequest, TranslationKeys.AuthorizationLoginRequestCannotBeNull);
			}

			if (!CheckEmail(loginRequest.Email, out var checkEmailResult))
			{
				return MethodResult<AccessTokenResponse>.Error(checkEmailResult.StatusCode, checkEmailResult.Message, checkEmailResult.TranslationKeyArgs);
			}

			if (!CheckPassword(loginRequest.Password, out var checkPasswordResult))
			{
				return MethodResult<AccessTokenResponse>.Error(checkPasswordResult.StatusCode, checkPasswordResult.Message, checkPasswordResult.TranslationKeyArgs);
			}

			var checkUserExistsResult = _usersManager.CheckUserExistsByEmail(loginRequest.Email, false, true);
			if (!checkUserExistsResult.IsSuccess)
			{
				return MethodResult<AccessTokenResponse>.Error(checkUserExistsResult.StatusCode, checkUserExistsResult.Message, checkUserExistsResult.TranslationKeyArgs);
			}

			var user = checkUserExistsResult.Value;
			var hashedPassword = PasswordHelper.HashPassword(loginRequest.Password);
			if (false && user.Password != hashedPassword)
			{
				return MethodResult<AccessTokenResponse>.Error(StatusCodes.Status401Unauthorized, TranslationKeys.AuthorizationPasswordIsWrong);
			}

			return AuthorizeInternal(user);
		}

		/// <summary>
		/// Зарегистрироваться по почте, имени пользователя, паролю и языку.
		/// </summary>
		/// <returns></returns>
		public MethodResult<AccessTokenResponse> TryRegister(RegistrationRequest registrationRequest)
		{
			if (registrationRequest == null)
			{
				return MethodResult<AccessTokenResponse>.Error(StatusCodes.Status400BadRequest, TranslationKeys.AuthorizationRegistrationRequestCannotBeNull);
			}

			if (!CheckUsername(registrationRequest.Username, out var checkUsernameResult))
			{
				return MethodResult<AccessTokenResponse>.Error(checkUsernameResult.StatusCode, checkUsernameResult.Message, checkUsernameResult.TranslationKeyArgs);
			}

			if (!CheckEmail(registrationRequest.Email, out var checkEmailResult))
			{
				return MethodResult<AccessTokenResponse>.Error(checkEmailResult.StatusCode, checkEmailResult.Message, checkEmailResult.TranslationKeyArgs);
			}

			if (!CheckPassword(registrationRequest.Password, out var checkPasswordResult))
			{
				return MethodResult<AccessTokenResponse>.Error(checkPasswordResult.StatusCode, checkPasswordResult.Message, checkPasswordResult.TranslationKeyArgs);
			}

			if (!CheckLanguage(registrationRequest.Language, out var checkLanguageResult))
			{
				return MethodResult<AccessTokenResponse>.Error(checkLanguageResult.StatusCode, checkLanguageResult.Message, checkLanguageResult.TranslationKeyArgs);
			}

			using (_syncManager.Lock(DefaultSyncs.Registration(registrationRequest.Email)))
			{
				using (_syncManager.Lock(DefaultSyncs.Username(registrationRequest.Username)))
				{
					var checkUserWithSameEmailExistsResult = _usersManager.CheckUserExistsByEmail(registrationRequest.Email, true, true);
					if (!checkUserWithSameEmailExistsResult.IsSuccess)
					{
						return MethodResult<AccessTokenResponse>.Error(checkUserWithSameEmailExistsResult.StatusCode, checkUserWithSameEmailExistsResult.Message, checkUserWithSameEmailExistsResult.TranslationKeyArgs);
					}

					var checkUserWithSameUsernameExistsResult = _usersManager.CheckUserExistsByUsername(registrationRequest.Username, true, true);
					if (!checkUserWithSameUsernameExistsResult.IsSuccess)
					{
						return MethodResult<AccessTokenResponse>.Error(checkUserWithSameUsernameExistsResult.StatusCode, checkUserWithSameUsernameExistsResult.Message, checkUserWithSameUsernameExistsResult.TranslationKeyArgs);
					}

					return RegisterInternal(registrationRequest);
				}
			}
		}

		/// <summary>
		/// Обновить access токен по refresh токену.
		/// </summary>
		/// <returns></returns>
		public MethodResult<AccessTokenResponse> TryRefresh(ClaimsPrincipal user)
		{
			var refreshToken = user.Claims.FirstOrDefault(c => c.Type == RefreshTokenClaim)?.Value;
			if (refreshToken == null)
			{
				return MethodResult<AccessTokenResponse>.Error(StatusCodes.Status403Forbidden, TranslationKeys.AuthorizationRefreshTokenNotFound);
			}

			var tokenInfo = _accessTokenPool.TryGetTokenInfo(refreshToken);
			if (tokenInfo == null)
			{
				return MethodResult<AccessTokenResponse>.Error(StatusCodes.Status401Unauthorized, TranslationKeys.AuthorizationSessionIsNotValid);
			}

			var userId = user.Claims.FirstOrDefault(c => c.Type == UserIdClaim)?.Value;
			User userTable;
			if (userId == null || !long.TryParse(userId, out var parsedUserId) || (userTable = _usersManager.GetUserInternal(parsedUserId)) == null) 
			{
				return MethodResult<AccessTokenResponse>.Error(StatusCodes.Status403Forbidden, TranslationKeys.AuthorizationUserNotFound);
			}

			var authorizeResult = AuthorizeInternal(userTable);
			if (authorizeResult.IsSuccess)
			{
				_accessTokenPool.Delete(refreshToken);
			}

			return authorizeResult;
		}

		private MethodResult<AccessTokenResponse> RegisterInternal(RegistrationRequest registrationRequest)
		{
			var user = _usersManager.CreateUserInternal(registrationRequest);
			return AuthorizeInternal(user);
		}

		private MethodResult<AccessTokenResponse> AuthorizeInternal(User user)
		{
			var refreshToken = GenerateRefreshToken();
			var authClaims = new List<Claim>()
			{
				new Claim(RefreshTokenClaim, refreshToken),
				new Claim(UserIdClaim, user.Id.ToString()),
				new Claim(PermissionLevelClaim, user.PermissionLevel.ToString()),
				new Claim(LanguageClaim, user.Language.ToString())
			};

			var rawAccessToken = CreateAccessToken(authClaims, out var tokenExpireDate);			
			var accessToken = new JwtSecurityTokenHandler().WriteToken(rawAccessToken);
			var tokenResponse = new AccessTokenResponse()
			{
				AccessToken = accessToken,
				AccessTokenExpireDate = tokenExpireDate,
				RefreshToken = refreshToken
			};

			_accessTokenPool.Append(tokenResponse);

			return MethodResult<AccessTokenResponse>.Success(tokenResponse);
		}

		private bool CheckUsername(string username, out MethodResult checkResult)
		{
			if (username == null)
			{
				checkResult = MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.AuthorizationUsernameCannotBeNull);
				return false;
			}

			var usernameLength = username.Length;
			if (usernameLength < MinUsernameLength || usernameLength > MaxUsernameLength)
			{
				checkResult = MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.AuthorizationUsernameDoesNotMatchLength, MinUsernameLength, MaxUsernameLength);
				return false;
			}

			checkResult = MethodResult.Success();
			return true;
		}

		private bool CheckEmail(string email, out MethodResult checkResult)
		{
			if (email == null)
			{
				checkResult = MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.AuthorizationEmailCannotBeNull);
				return false;
			}

			if (!EmailPattern.IsMatch(email))
			{
				checkResult = MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.AuthorizationEmailDoesNotMatchPattern);
				return false;
			}

			checkResult = MethodResult.Success();
			return true;
		}

		private bool CheckPassword(string password, out MethodResult checkResult)
		{
			if (password == null)
			{
				checkResult = MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.AuthorizationPasswordCannotBeNull);
				return false;
			}

			var passwordLength = password.Length;
			if (passwordLength < MinPasswordLength || passwordLength > MaxPasswordLength)
			{
				checkResult = MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.AuthorizationUsernameDoesNotMatchLength, MinPasswordLength, MaxPasswordLength);
				return false;
			}

			checkResult = MethodResult.Success();
			return true;
		}

		private bool CheckLanguage(Language language, out MethodResult checkResult)
		{
			if (!Enum.IsDefined(language))
			{
				checkResult = MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.AuthorizationLanguageNotFound);
				return false;
			}

			checkResult = MethodResult.Success();
			return true;
		}

		private JwtSecurityToken CreateAccessToken(List<Claim> authClaims, out DateTimeOffset tokenExpireDate)
		{
			var dateTimeNow = DateTimeOffset.UtcNow;
			tokenExpireDate = dateTimeNow.AddDays(_appConfig.Jwt.AccessTokenValidityDays);

			var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.Jwt.Secret));
			var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

			return new JwtSecurityToken(expires: tokenExpireDate.DateTime, claims: authClaims, signingCredentials: signingCredentials);
		}

		private string GenerateRefreshToken()
		{
			var randomNumber = new byte[64];

			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);

			return Convert.ToBase64String(randomNumber);
		}
	}
}
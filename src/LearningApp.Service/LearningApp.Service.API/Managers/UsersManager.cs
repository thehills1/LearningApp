using System.Linq;
using AutoMapper;
using LearningApp.Service.API.Contracts.Authorization.Requests;
using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.API.Contracts.Users.Requests;
using LearningApp.Service.API.Contracts.Users.Responses;
using LearningApp.Service.API.Entities;
using LearningApp.Service.API.Utils;
using LearningApp.Service.Core.Extensions;
using LearningApp.Service.Database.Repositories;
using LearningApp.Service.Database.Tables;
using LearningApp.Service.Langs.API;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LearningApp.Service.API.Managers
{
	public class UsersManager : IUsersManager
	{
		private readonly ILogger<UsersManager> _logger;
		private readonly IDbRepository _dbRepository;
		private readonly ICredentialsManager _credentialsManager;
		private readonly IMapper _mapper;

		public UsersManager(
			ILogger<UsersManager> logger, 
			IDbRepository dbRepository, 
			ICredentialsManager credentialsManager, 
			IMapper mapper)
		{
			_logger = logger;
			_dbRepository = dbRepository;
			_credentialsManager = credentialsManager;
			_mapper = mapper;
		}

		public MethodResult<UserInfoResponse> TryGetUserById(long id, long initiatorId, PermissionLevel initiatorPermissionLevel)
		{
			if (id != initiatorId && initiatorPermissionLevel < PermissionLevel.Admin)
			{
				return MethodResult<UserInfoResponse>.Error(StatusCodes.Status403Forbidden, TranslationKeys.CommonYouHasNoPerms);
			}

			var checkUserExistsResult = CheckUserExists(id, true);
			if (!checkUserExistsResult.IsSuccess)
			{
				return checkUserExistsResult.ToResult<UserInfoResponse>();
			}

			return MethodResult<UserInfoResponse>.Success(_mapper.Map<UserInfoResponse>(checkUserExistsResult.Value));
		}

		public MethodResult TryChangePassword(long userId, ChangePasswordRequest request)
		{
			if (request == null)
			{
				return MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.UsersChangePasswordRequestCannotBeNull);
			}

			if (!_credentialsManager.CheckPasswordSyntax(request.Password, out var checkOldPasswordResult))
			{
				return checkOldPasswordResult;
			}

			if (!_credentialsManager.CheckPasswordSyntax(request.NewPassword, out var checkNewPasswordResult, true))
			{
				return checkNewPasswordResult;
			}

			var user = GetUserInternal(userId, false);
			if (!_credentialsManager.CheckPassword(user, request.Password))
			{
				return MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.CredentialsPasswordIsWrong);
			}

			_credentialsManager.ChangePassword(user, request.NewPassword);

			return MethodResult.Success();
		}

		public User CreateUserInternal(RegistrationRequest registrationRequest)
		{
			var user = new User()
			{
				Email = registrationRequest.Email,
				Username = registrationRequest.Username,
				Password = PasswordHelper.HashPassword(registrationRequest.Password),
				Language = registrationRequest.Language,
				PermissionLevel = PermissionLevel.User
			};

			_dbRepository.Add(user);

			return user;
		}

		public User GetUserInternal(long id, bool noTracking = true)
		{
			return _dbRepository.Get<User>(id, noTracking);
		}

		public MethodResult<User> CheckUserExistsByEmail(string email, bool errorIfExists, bool noTraking = true)
		{
			if (email.IsNullOrEmpty()) return MethodResult<User>.Error(StatusCodes.Status400BadRequest, TranslationKeys.UsersEmailCannotBeNull);

			var user = _dbRepository.Get<User>(t => t.Email == email, noTraking).FirstOrDefault();
			if (errorIfExists && user != null)
			{
				return MethodResult<User>.Error(StatusCodes.Status409Conflict, TranslationKeys.UsersUserWithSameEmailAlreadyExists, email);
			}

			if (!errorIfExists && user == null)
			{
				return MethodResult<User>.Error(StatusCodes.Status404NotFound, TranslationKeys.UsersUserWithSameEmailNotExists, email);
			}

			return MethodResult<User>.Success(user);
		}

		public MethodResult<User> CheckUserExistsByUsername(string username, bool errorIfExists, bool noTraking = true)
		{
			if (username.IsNullOrEmpty()) return MethodResult<User>.Error(StatusCodes.Status400BadRequest, TranslationKeys.UsersUsernameCannotBeNull);

			var user = _dbRepository.Get<User>(t => t.Username == username, noTraking).FirstOrDefault();
			if (errorIfExists && user != null)
			{
				return MethodResult<User>.Error(StatusCodes.Status409Conflict, TranslationKeys.UsersUserWithSameUsernameAlreadyExists, username);
			}

			if (!errorIfExists && user == null)
			{
				return MethodResult<User>.Error(StatusCodes.Status409Conflict, TranslationKeys.UsersUserWithSameUsernameNotExists, username);
			}

			return MethodResult<User>.Success(user);
		}

		public MethodResult<User> CheckUserExists(long id, bool noTracking = true)
		{
			if (id <= 0)
			{
				return MethodResult<User>.Error(StatusCodes.Status400BadRequest, TranslationKeys.UsersUserIdCannotBeLessOrEqualToZero);
			}

			var user = GetUserInternal(id);
			if (user == null)
			{
				return MethodResult<User>.Error(StatusCodes.Status404NotFound, TranslationKeys.UsersUserNotFound);
			}

			return MethodResult<User>.Success(user);
		}
	}
}
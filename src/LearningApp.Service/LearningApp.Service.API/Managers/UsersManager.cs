using AutoMapper;
using LearningApp.Service.API.Contracts.Authorization.Requests;
using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.API.Contracts.Users.Requests;
using LearningApp.Service.API.Contracts.Users.Responses;
using LearningApp.Service.API.Entities;
using LearningApp.Service.API.Utils;
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
		private readonly IUsersManagerInternal _usersManagerInternal;
		private readonly ICredentialsManager _credentialsManager;
		private readonly IQuestionsManager _questionsManager;
		private readonly IMapper _mapper;

		public UsersManager(
			ILogger<UsersManager> logger, 
			IDbRepository dbRepository, 
			IUsersManagerInternal usersManagerInternal,
			ICredentialsManager credentialsManager,
			IQuestionsManager questionsManager,
			IMapper mapper)
		{
			_logger = logger;
			_dbRepository = dbRepository;
			_usersManagerInternal = usersManagerInternal;
			_credentialsManager = credentialsManager;
			_questionsManager = questionsManager;
			_mapper = mapper;
		}

		public MethodResult<UserInfoResponse> TryGetUserById(long id, long initiatorId, PermissionLevel initiatorPermissionLevel)
		{
			if (id != initiatorId && initiatorPermissionLevel < PermissionLevel.Admin)
			{
				return MethodResult<UserInfoResponse>.Error(StatusCodes.Status403Forbidden, TranslationKeys.CommonYouHasNoPerms);
			}

			var checkUserExistsResult = _usersManagerInternal.CheckUserExists(id, true);
			if (!checkUserExistsResult.IsSuccess)
			{
				return checkUserExistsResult.ToResult<UserInfoResponse>();
			}

			var getLastSubmissionsResult = _questionsManager.GetLastSubmissionsInfo(id);
			if (!getLastSubmissionsResult.IsSuccess)
			{
				return getLastSubmissionsResult.ToResult<UserInfoResponse>();
			}

			var userInfoResponse = _mapper.Map<UserInfoResponse>(checkUserExistsResult.Value);
			userInfoResponse.LastSubmissionsInfo = getLastSubmissionsResult.Value;

			return MethodResult<UserInfoResponse>.Success(userInfoResponse);
		}

		public MethodResult TryChangePassword(long userId, ChangePasswordRequest request)
		{
			if (request == null)
			{
				return MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.UsersChangePasswordRequestCanNotBeNull);
			}

			if (!_credentialsManager.CheckPasswordSyntax(request.Password, out var checkOldPasswordResult))
			{
				return checkOldPasswordResult;
			}

			if (!_credentialsManager.CheckPasswordSyntax(request.NewPassword, out var checkNewPasswordResult, true))
			{
				return checkNewPasswordResult;
			}

			var user = _usersManagerInternal.GetUser(userId, false);
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
	}
}
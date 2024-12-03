using LearningApp.Service.API.Contracts.Authorization.Requests;
using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.API.Contracts.Users.Requests;
using LearningApp.Service.API.Contracts.Users.Responses;
using LearningApp.Service.API.Entities;
using LearningApp.Service.Database.Tables;

namespace LearningApp.Service.API.Managers
{
	public interface IUsersManager
	{
		MethodResult<User> CheckUserExists(long id, bool noTracking = true);
		MethodResult<User> CheckUserExistsByEmail(string email, bool errorIfExists, bool noTraking = true);
		MethodResult<User> CheckUserExistsByUsername(string username, bool errorIfExists, bool noTraking = true);
		User CreateUserInternal(RegistrationRequest registrationRequest);
		User GetUserInternal(long id, bool noTracking = true);
		MethodResult TryChangePassword(long userId, ChangePasswordRequest request);
		MethodResult<UserInfoResponse> TryGetUserById(long id, long initiatorId, PermissionLevel initiatorPermissionLevel);
	}
}
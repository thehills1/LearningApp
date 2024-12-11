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
		User CreateUserInternal(RegistrationRequest registrationRequest);
		MethodResult TryChangePassword(long userId, ChangePasswordRequest request);
		MethodResult<UserInfoResponse> TryGetUserById(long id, long initiatorId, PermissionLevel initiatorPermissionLevel);
	}
}
using LearningApp.Service.API.Contracts.Authorization.Requests;
using LearningApp.Service.API.Entities;
using LearningApp.Service.Database.Tables;

namespace LearningApp.Service.API.Managers
{
	public interface IUsersManager
	{
		MethodResult<User> CheckUserExistsByEmail(string email, bool errorIfExists, bool noTraking = true);
		MethodResult<User> CheckUserExistsByUsername(string username, bool errorIfExists, bool noTraking = true);
		User CreateUserInternal(RegistrationRequest registrationRequest);
		User GetUserInternal(long id, bool noTracking = true);
	}
}
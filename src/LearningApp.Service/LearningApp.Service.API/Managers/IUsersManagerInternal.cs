using LearningApp.Service.API.Entities;
using LearningApp.Service.Database.Tables;

namespace LearningApp.Service.API.Managers
{
	public interface IUsersManagerInternal
	{
		MethodResult<User> CheckUserExists(long id, bool noTracking = true);
		MethodResult<User> CheckUserExistsByEmail(string email, bool errorIfExists, bool noTraking = true);
		MethodResult<User> CheckUserExistsByUsername(string username, bool errorIfExists, bool noTraking = true);
		User GetUser(long id, bool noTracking = true);
	}
}
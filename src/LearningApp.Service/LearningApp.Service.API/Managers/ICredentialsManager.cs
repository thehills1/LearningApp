using LearningApp.Service.API.Entities;
using LearningApp.Service.Database.Tables;

namespace LearningApp.Service.API.Managers
{
	public interface ICredentialsManager
	{
		void ChangePassword(User user, string password);
		bool CheckPassword(User user, string password);
		bool CheckPasswordSyntax(string password, out MethodResult checkResult, bool newPassword = false);
	}
}
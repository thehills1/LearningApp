using LearningApp.Service.API.Utils;

namespace LearningApp.Service.API.Managers
{
	public interface IUserSessionsManager
	{
		UserSession GetOrAdd(long userId);
		bool TryRemove(long userId);
	}
}
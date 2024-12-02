namespace LearningApp.Service.Core.Syncs
{
	public interface ISyncManager
	{
		Locker Lock(string key);
		void EnterSync(string key);
		bool IsEntered(string key);
		bool TryExit(string key);
	}
}
using System.Collections.Concurrent;
using LearningApp.Service.API.Utils;
using Microsoft.Extensions.Logging;

namespace LearningApp.Service.API.Managers
{
	public class UserSessionsManager : IUserSessionsManager
	{
		private readonly ConcurrentDictionary<long, UserSession> _currentSessions = new();

		private readonly ILogger<UserSessionsManager> _logger;

		public UserSessionsManager(ILogger<UserSessionsManager> logger)
		{
			_logger = logger;
		}

		public UserSession GetOrAdd(long userId)
		{
			if (!_currentSessions.TryGetValue(userId, out var session))
			{
				_currentSessions.TryAdd(userId, session = new UserSession(userId));
			}

			return session;
		}

		public bool TryRemove(long userId)
		{
			return _currentSessions.TryRemove(userId, out _);
		}
	}
}
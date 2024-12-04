using System.Collections.Generic;

namespace LearningApp.Service.API.Utils
{
	public class UserSession
	{
		public long UserId { get; }

		public HashSet<long> AnsweringQuestions => new(_answeringQuestions);

		private readonly HashSet<long> _answeringQuestions = new();

		public UserSession(long userId)
		{
			UserId = userId;
		}

		public void AddAnsweringQuestion(long questionId)
		{
			lock(_answeringQuestions)
			{
				_answeringQuestions.Add(questionId);
			}
		}

		public void RemoveAnsweringQuestion(long questionId)
		{
			lock(_answeringQuestions)
			{
				_answeringQuestions.Remove(questionId);
			}
		}

		public bool IsAnswering(long questionId)
		{
			lock (_answeringQuestions)
			{
				return _answeringQuestions.Contains(questionId);
			}
		}
	}
}
namespace LearningApp.Service.API
{
	public static class DefaultSyncs
	{
		public static string Registration(string email) => $"registration_email_{email}";

		public static string Username(string username) => $"username_{username}";

		public static string GetNextQuestion(long userId) => $"get_next_question_{userId}";

		public static string SubmitQuestionAnswer(long userId, long questionId) => $"submit_question_answer_user_{userId}_question_{questionId}";
	}
}
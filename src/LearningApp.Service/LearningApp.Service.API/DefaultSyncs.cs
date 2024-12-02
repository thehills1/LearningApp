namespace LearningApp.Service.API
{
	public static class DefaultSyncs
	{
		public static string Registration(string email) => $"registration_email_{email}";
		public static string Username(string username) => $"username_{username}";
	}
}
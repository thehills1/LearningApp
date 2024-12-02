using LearningApp.Service.API.Contracts.Users.Common;

namespace LearningApp.Service.API.Contracts.Authorization.Requests
{
	public class RegistrationRequest
	{
		public string Email { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public Language Language { get; set; }
	}
}
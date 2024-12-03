namespace LearningApp.Service.API.Contracts.Users.Requests
{
	public class ChangePasswordRequest
	{
		public string Password { get; set; }

		public string NewPassword { get; set; }
	}
}
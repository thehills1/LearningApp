namespace LearningApp.Service.API.Contracts.Authorization.Requests
{
	public sealed class LoginRequest
	{
		public string Email { get; init; }

		public string Password { get; init; }
	}
}
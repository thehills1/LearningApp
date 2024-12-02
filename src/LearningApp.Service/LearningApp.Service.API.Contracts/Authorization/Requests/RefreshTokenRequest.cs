namespace LearningApp.Service.API.Contracts.Authorization.Requests
{
	public class RefreshTokenRequest
	{
		public string AccessToken { get; init; }

		public string RefreshToken { get; init; }
	}
}
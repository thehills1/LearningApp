using System;

namespace LearningApp.Service.API.Contracts.Authorization.Responses
{
	public class AccessTokenResponse
	{
		public string TokenType { get; } = "Bearer";

		public string AccessToken { get; init; }

		public DateTimeOffset AccessTokenExpireDate { get; init; }

		public string RefreshToken { get; init; }
	}
}
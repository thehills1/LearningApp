using System;
using System.Collections.Concurrent;
using LearningApp.Service.API.Contracts.Authorization.Responses;

namespace LearningApp.Service.API.Utils
{
	public class AccessTokenPool : IAccessTokenPool
	{
		private readonly ConcurrentDictionary<string, AccessTokenResponse> _activeTokens = new();

		public void Append(AccessTokenResponse tokenResponse)
		{
			_activeTokens.TryAdd(tokenResponse.RefreshToken, tokenResponse);
		}

		public void Delete(string refreshToken)
		{
			_activeTokens.TryRemove(refreshToken, out _);
		}

		public AccessTokenResponse TryGetTokenInfo(string refreshToken)
		{
			_activeTokens.TryGetValue(refreshToken, out var tokenInfo);
			return tokenInfo;
		}

		public bool IsExpired(string refreshToken)
		{
			return !_activeTokens.TryGetValue(refreshToken, out var accessTokenResponse) || accessTokenResponse.AccessTokenExpireDate < DateTimeOffset.UtcNow;
		}
	}
}
using LearningApp.Service.API.Contracts.Authorization.Responses;

namespace LearningApp.Service.API.Utils
{
	public interface IAccessTokenPool
	{
		void Append(AccessTokenResponse tokenResponse);
		void Delete(string refreshToken);
		AccessTokenResponse TryGetTokenInfo(string refreshToken);
	}
}
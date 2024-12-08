using System.Security.Claims;
using LearningApp.Service.API.Contracts.Authorization.Requests;
using LearningApp.Service.API.Contracts.Authorization.Responses;
using LearningApp.Service.API.Entities;

namespace LearningApp.Service.API.Managers
{
	public interface IAuthorizationManager
	{
		MethodResult<AccessTokenResponse> TryLogin(LoginRequest loginRequest);
		MethodResult TryLogout(ClaimsPrincipal user);
		MethodResult<AccessTokenResponse> TryRefresh(ClaimsPrincipal user);
		MethodResult<AccessTokenResponse> TryRegister(RegistrationRequest registrationRequest);
	}
}
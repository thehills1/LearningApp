using LearningApp.Service.API.Contracts.Authorization.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.Service.API.Contracts.Authorization
{
	public interface IAuthorizationController
	{
		IActionResult Login([FromBody] LoginRequest loginRequest);
		IActionResult Refresh();
		IActionResult Register([FromBody] RegistrationRequest registrationRequest);
	}
}
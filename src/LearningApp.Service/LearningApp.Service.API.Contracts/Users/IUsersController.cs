using LearningApp.Service.API.Contracts.Users.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.Service.API.Contracts.Users
{
	public interface IUsersController
	{
		IActionResult ChangePassword([FromBody] ChangePasswordRequest changeRequest);
		IActionResult GetProfile();
		IActionResult GetUserById(long userId);
	}
}
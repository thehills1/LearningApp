using LearningApp.Service.API.Contracts.Users.Requests;
using LearningApp.Service.API.Contracts.Users.Responses;
using LearningApp.Service.API.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningApp.Service.API.Controllers
{
	[Authorize]
	[Route("[controller]")]
	[ApiController]
	public class UsersController : ApiControllerBase
	{
		private readonly IUsersManager _usersManager;

		public UsersController(ILogger<UsersController> logger, IUsersManager usersManager) : base(logger)
		{
			_usersManager = usersManager;
		}

		/// <summary>
		/// Получить информацию о пользователе по его идентификатору.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		/// <response code="200">Информация о пользователе</response>
		/// <response code="400">Указан некорректный id пользователя</response>
		/// <response code="401">Не авторизован</response>
		/// <response code="403">Нет доступа к просмотру информации об указанном пользователе</response>
		/// <response code="404">Пользователь с указанным id не найден</response>
		[HttpPost("{userId}")]
		[ProducesResponseType<UserInfoResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]		
		[ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
		public IActionResult GetUserById(long userId)
		{
			return _usersManager.TryGetUserById(userId, CurrentUserId, CurrentUserPerms).ToActionResult(CurrentUserLanguage);
		}

		/// <summary>
		/// Обновить пароль от аккаунта.
		/// </summary>
		/// <param name="changeRequest"></param>
		/// <returns></returns>
		/// <response code="200"></response>
		/// <response code="400">Указан неверный пароль или некорректный новый пароль</response>
		/// <response code="401">Не авторизован</response>
		[HttpPut("password")]
		[ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
		public IActionResult ChangePassword([FromBody] ChangePasswordRequest changeRequest)
		{
			return _usersManager.TryChangePassword(CurrentUserId, changeRequest).ToActionResult(CurrentUserLanguage);
		}
	}
}
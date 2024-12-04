using LearningApp.Service.API.Contracts.Authorization;
using LearningApp.Service.API.Contracts.Authorization.Requests;
using LearningApp.Service.API.Contracts.Authorization.Responses;
using LearningApp.Service.API.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningApp.Service.API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthorizationController : ApiControllerBase, IAuthorizationController
	{
		private readonly IAuthorizationManager _authorizationManager;

		public AuthorizationController(ILogger<AuthorizationController> logger, IAuthorizationManager authorizationManager) : base(logger)
		{
			_authorizationManager = authorizationManager;
		}

		/// <summary>
		/// Авторизоваться по почте и паролю.
		/// </summary>
		/// <param name="loginRequest"></param>
		/// <returns></returns>
		/// <response code="200">Информация об access токене</response>
		/// <response code="400">Указаны некорректные данные</response>
		/// <response code="401">Неверный пароль</response>
		/// <response code="404">Пользователь с указанной почтой не найден</response>
		[HttpPost("login")]
		[ProducesResponseType<AccessTokenResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
		public IActionResult Login([FromBody] LoginRequest loginRequest)
		{
			return _authorizationManager.TryLogin(loginRequest).ToActionResult(loginRequest?.Language ?? CurrentUserLanguage);
		}

		/// <summary>
		/// Зарегистрироваться по почте, имени пользователя, паролю и языку.
		/// </summary>
		/// <param name="registrationRequest"></param>
		/// <returns></returns>
		/// <response code="200">Информация об access токене</response>
		/// <response code="400">Указаны некорректные данные</response>
		/// <response code="409">Пользователь с указанными данными уже зарегистрирован</response>
		[HttpPost("register")]
		[ProducesResponseType<AccessTokenResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
		public IActionResult Register([FromBody] RegistrationRequest registrationRequest)
		{
			return _authorizationManager.TryRegister(registrationRequest).ToActionResult(registrationRequest?.Language ?? CurrentUserLanguage);
		}

		/// <summary>
		/// Обновить access токен сессии по refresh токену.
		/// </summary>
		/// <returns></returns>
		/// <response code="401">Сессия не валидна</response>
		/// <response code="403">Некорректные параметры сессии</response>
		[Authorize]
		[HttpPost("refresh")]
		[ProducesResponseType<AccessTokenResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
		public IActionResult Refresh()
		{
			return _authorizationManager.TryRefresh(User).ToActionResult(CurrentUserLanguage);
		}
	}
}
using LearningApp.Service.API.Contracts.Questions;
using LearningApp.Service.API.Contracts.Questions.Common;
using LearningApp.Service.API.Contracts.Questions.Requests;
using LearningApp.Service.API.Contracts.Questions.Responses;
using LearningApp.Service.API.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningApp.Service.API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class QuestionsController : ApiControllerBase, IQuestionsController
	{
		private readonly IQuestionsManager _questionsManager;

		public QuestionsController(ILogger<QuestionsController> logger, IQuestionsManager questionsManager) : base(logger)
		{
			_questionsManager = questionsManager;
		}

		/// <summary>
		/// Получить следующий вопрос, который можно решить.
		/// </summary>
		/// <param name="difficulty"></param>
		/// <returns></returns>
		/// <response code="200">Информация о вопросе</response>
		/// <response code="204">Пользователь уже ответил на все возможные вопросы за промежуток времени</response>
		/// <response code="400">Некорректный тип сложности вопроса</response>
		/// <response code="401">Не авторизован</response>
		[HttpGet("next")]
		[ProducesResponseType<QuestionInfoResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
		public IActionResult GetNextQuestion(QuestionDifficulty difficulty)
		{
			return _questionsManager.TryGetNextQuestion(CurrentUserId, difficulty, CurrentUserLanguage).ToActionResult(CurrentUserLanguage);
		}

		/// <summary>
		/// Получить вопрос по его идентификатору.
		/// </summary>
		/// <param name="questionId"></param>
		/// <returns></returns>
		/// <response code="200">Информация о вопросе</response>
		/// <response code="400">Некорректный id вопроса</response>
		/// <response code="401">Не авторизован</response>
		/// <response code="403">Нет доступа к просмотру данного вопроса</response>
		/// <response code="404">Вопрос не найден</response>
		[HttpGet("{questionId}")]
		[ProducesResponseType<QuestionInfoResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
		public IActionResult GetQuestionById(long questionId)
		{
			return _questionsManager.TryGetQuestionById(CurrentUserId, CurrentUserPerms, questionId, CurrentUserLanguage).ToActionResult(CurrentUserLanguage);
		}

		/// <summary>
		/// Ответить на вопрос, либо отказаться от его решения.
		/// </summary>
		/// <param name="submitRequest"></param>
		/// <returns></returns>
		/// <response code="200">Результат ответа на вопрос, если был совершен ответ</response>
		/// <response code="400">Некорректный id вопроса/ответа</response>
		/// <response code="401">Не авторизован</response>
		/// <response code="403">Нет доступа к ответу на данный вопрос</response>
		/// <response code="404">Вопрос/ответ не найден</response>
		[HttpPost("submit")]
		[ProducesResponseType<SubmitQuestionAnswerResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
		public IActionResult SubmitQuestionAnswer([FromBody] SubmitQuestionAnswerRequest submitRequest)
		{
			return _questionsManager.TrySubmitQuestionAnswer(CurrentUserId, submitRequest).ToActionResult(CurrentUserLanguage);
		}
	}
}
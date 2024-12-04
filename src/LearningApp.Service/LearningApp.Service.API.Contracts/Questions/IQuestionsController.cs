using LearningApp.Service.API.Contracts.Questions.Common;
using LearningApp.Service.API.Contracts.Questions.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.Service.API.Contracts.Questions
{
	public interface IQuestionsController
	{
		IActionResult GetNextQuestion(QuestionDifficulty difficulty);
		IActionResult GetQuestionById(long questionId);
		IActionResult SubmitQuestionAnswer([FromBody] SubmitQuestionAnswerRequest submitRequest);
	}
}
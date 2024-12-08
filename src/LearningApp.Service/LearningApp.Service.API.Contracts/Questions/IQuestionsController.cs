using LearningApp.Service.API.Contracts.Questions.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.Service.API.Contracts.Questions
{
	public interface IQuestionsController
	{
		IActionResult GetNextQuestion([FromBody] GetNextQuestionRequest request);
		IActionResult GetQuestionById(long questionId);
		IActionResult SubmitQuestionAnswer([FromBody] SubmitQuestionAnswerRequest submitRequest);
	}
}
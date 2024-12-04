using LearningApp.Service.API.Contracts.Questions.Common;
using LearningApp.Service.API.Contracts.Questions.Requests;
using LearningApp.Service.API.Contracts.Questions.Responses;
using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.API.Entities;

namespace LearningApp.Service.API.Managers
{
	public interface IQuestionsManager
	{
		MethodResult<QuestionInfoResponse> TryGetNextQuestion(long userId, QuestionDifficulty difficulty, Language language);
		MethodResult<QuestionInfoResponse> TryGetQuestionById(long userId, PermissionLevel userPerms, long questionId, Language language);
		MethodResult<SubmitQuestionAnswerResponse> TrySubmitQuestionAnswer(long userId, SubmitQuestionAnswerRequest submitRequest);
	}
}
using LearningApp.Service.API.Contracts.Questions.Common;

namespace LearningApp.Service.API.Contracts.Questions.Requests
{
	public class GetNextQuestionRequest
	{
		public QuestionDifficulty Difficulty { get; set; }

		public ProgrammingLanguage ProgrammingLanguage { get; set; }
	}
}

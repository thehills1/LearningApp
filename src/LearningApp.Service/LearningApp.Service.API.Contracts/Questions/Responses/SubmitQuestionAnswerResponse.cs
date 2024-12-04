using LearningApp.Service.API.Contracts.Questions.Common;

namespace LearningApp.Service.API.Contracts.Questions.Responses
{
	public class SubmitQuestionAnswerResponse
	{
		public SubmitAnswerStatus Status { get; set; }

		public double ReceivedScore { get; set; }
	}
}
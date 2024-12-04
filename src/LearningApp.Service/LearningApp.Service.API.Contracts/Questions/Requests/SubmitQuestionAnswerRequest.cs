using System.Collections.Generic;
using LearningApp.Service.API.Contracts.Questions.Common;

namespace LearningApp.Service.API.Contracts.Questions.Requests
{
	public class SubmitQuestionAnswerRequest
	{
		public SubmitAction SubmitAction { get; set; } 

		public long QuestionId { get; set; }

		public List<long> AnswerIds { get; set; }
	}
}
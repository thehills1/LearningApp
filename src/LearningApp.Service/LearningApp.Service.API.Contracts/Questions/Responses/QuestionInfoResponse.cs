using System.Collections.Generic;
using LearningApp.Service.API.Contracts.Questions.Common;

namespace LearningApp.Service.API.Contracts.Questions.Responses
{
	public class QuestionInfoResponse
	{
		public long Id { get; set; }

		public QuestionDifficulty Difficulty { get; set; }

		public ProgrammingLanguage ProgrammingLanguage { get; set; }

		public double MaxScore { get; set; }

		public string Title { get; set; }

		public string ImageBase64 { get; set; }

		public bool MultipleCorrectAnswers { get; set; }

		public List<AnswerInfo> Answers { get; set; }
	}

	public class AnswerInfo
	{
		public long Id { get; set; }

		public string Title { get; set; }

		public string ImageBase64 { get; set; }
	}
}
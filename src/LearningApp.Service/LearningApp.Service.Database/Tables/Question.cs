using System.ComponentModel.DataAnnotations;
using LearningApp.Service.API.Contracts.Questions.Common;

namespace LearningApp.Service.Database.Tables
{
	public class Question : TableBase
	{
		[Required]
		public long TitleId { get; set; }

		public long? ImageId { get; set; }

		[Required]
		public QuestionDifficulty Difficulty { get; set; }

		[Required]
		public bool MultipleCorrectAnswers { get; set; }

		[Required]
		public double MaxScore { get; set; }
	}
}
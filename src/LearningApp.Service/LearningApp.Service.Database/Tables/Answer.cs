using System.ComponentModel.DataAnnotations;

namespace LearningApp.Service.Database.Tables
{
	public class Answer : TableBase
	{
		[Required]
		public long TitleId { get; set; }

		public long? ImageId { get; set; }

		[Required]
		public long QuestionId { get; set; }

		[Required]
		public bool IsCorrect { get; set; }
	}
}
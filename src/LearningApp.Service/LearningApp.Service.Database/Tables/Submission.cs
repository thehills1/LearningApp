using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearningApp.Service.Database.Tables
{
	public class Submission : TableBase
	{
		[Required]
		public long UserId { get; set; }

		[Required]
		public long AnswerId { get; set; }

		[Required]
		public double ReceivedScore { get; set; }

		[Required]
		[Column(TypeName = "timestamp with time zone")]
		public DateTimeOffset SubmissionDate { get; set; } = DateTimeOffset.UtcNow;
	}
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningApp.Service.Database.Tables
{
	public class Title : TableBase
	{
		[Required]
		[Column(TypeName = "VARCHAR(1024)")]
		public string Russian { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(1024)")]
		public string English { get; set; }
	}
}
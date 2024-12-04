using System.ComponentModel.DataAnnotations;

namespace LearningApp.Service.Database.Tables
{
	public class Image : TableBase
	{
		[Required]
		public string Base64 { get; set; }
	}
}
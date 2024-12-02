using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LearningApp.Service.API.Contracts.Users.Common;

namespace LearningApp.Service.Database.Tables
{
	public class User : TableBase
	{
		[Column(TypeName = "VARCHAR(50)")]
		[Required]
		public string Email { get; set; }

		[Column(TypeName = "VARCHAR(15)")]
		[Required]
		public string Username { get; set; }

		[Column(TypeName = "VARCHAR(16384)")]
		[Required]
		public string Password { get; set; }

		[Required]
		public PermissionLevel PermissionLevel { get; set; }

		[Required]
		public Language Language { get; set; }
	}
}
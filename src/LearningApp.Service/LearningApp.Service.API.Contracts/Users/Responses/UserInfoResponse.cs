using System;
using LearningApp.Service.API.Contracts.Users.Common;

namespace LearningApp.Service.API.Contracts.Users.Responses
{
	public class UserInfoResponse
	{
		public long Id { get; set; }

		public string Email { get; set; }

		public string Username { get; set; }

		public PermissionLevel PermissionLevel { get; set; }

		public Language Language { get; set; }

		public DateTimeOffset RegistrationDate { get; set; }
	}
}
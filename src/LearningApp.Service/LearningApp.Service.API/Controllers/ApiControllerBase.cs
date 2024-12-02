using System;
using System.Linq;
using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.API.Managers;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.Service.API.Controllers
{
	public abstract class ApiControllerBase : ControllerBase
	{
		public Language Language
		{
			get
			{
				var rawLanguage = User.Claims.FirstOrDefault(c => c.Type == AuthorizationManager.LanguageClaim)?.Value;
				if (Enum.TryParse<Language>(rawLanguage, out var language)) return language;

				return Language.Russian;
			}
		}
	}
}

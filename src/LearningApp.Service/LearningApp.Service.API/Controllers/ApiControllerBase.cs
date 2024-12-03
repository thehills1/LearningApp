using System;
using System.Linq;
using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.API.Managers;
using LearningApp.Service.Langs.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LearningApp.Service.API.Controllers
{
	public abstract class ApiControllerBase : ControllerBase
	{
		public long CurrentUserId
		{
			get
			{
				var rawUserId = User.Claims.FirstOrDefault(c => c.Type == AuthorizationManager.UserIdClaim)?.Value;
				if (rawUserId == null || !long.TryParse(rawUserId, out var userId))
				{
					Logger.LogError("Unexpected null user id in claims\n{user}", JsonConvert.SerializeObject(User));
					return 0;
				}

				return userId;
			}
		}

		public PermissionLevel CurrentUserPerms
		{
			get
			{
				var rawPerms = User.Claims.FirstOrDefault(c => c.Type == AuthorizationManager.PermissionLevelClaim)?.Value;
				if (Enum.TryParse<PermissionLevel>(rawPerms, out var perms)) return perms;

				return PermissionLevel.User;
			}
		}

		public Language CurrentUserLanguage
		{
			get
			{
				var rawLanguage = User.Claims.FirstOrDefault(c => c.Type == AuthorizationManager.LanguageClaim)?.Value;
				if (Enum.TryParse<Language>(rawLanguage, out var language)) return language;

				return LangManager.DefaultLang;
			}
		}

		protected ILogger Logger { get; }

		public ApiControllerBase(ILogger logger)
		{
			Logger = logger;
		}		
	}
}
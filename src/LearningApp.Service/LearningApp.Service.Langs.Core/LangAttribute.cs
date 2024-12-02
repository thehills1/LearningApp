using System;
using LearningApp.Service.API.Contracts.Users.Common;

namespace LearningApp.Service.Langs.Core
{
	public class LangAttribute : Attribute
	{
		public Language Lang { get; }

		public LangAttribute(Language lang)
		{
			Lang = lang;
		}
	}
}
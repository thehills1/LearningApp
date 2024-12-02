using LearningApp.Service.API.Contracts.Users.Common;

namespace LearningApp.Service.Langs.Core.Extensions
{
	public static class StringExtension
	{
		public static string Translate(this string translationKey, Language lang, params object[] args)
		{
			return LangManager.GetTranslation(translationKey, lang, args);
		}

#nullable enable
		public static string? TryTranslate(this string translationKey, Language lang, params object[] args)
		{
			return LangManager.TryGetTranslation(translationKey, lang, args);
		}
#nullable restore
	}
}
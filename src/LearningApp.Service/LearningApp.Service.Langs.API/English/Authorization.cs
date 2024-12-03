using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.Langs.Core;

namespace LearningApp.Service.Langs.API.English
{
	[Lang(Language.English)]
	public class Authorization : LangBase
	{
		public Authorization()
		{
			this[TranslationKeys.AuthorizationLoginRequestCannotBeNull] = "Login request cannot be null.";
			this[TranslationKeys.AuthorizationRegistrationRequestCannotBeNull] = "Registration request cannot be null.";

			this[TranslationKeys.AuthorizationRefreshTokenNotFound] = "Refresh token not found.";
			this[TranslationKeys.AuthorizationSessionIsNotValid] = "Session is not valid.";
			this[TranslationKeys.AuthorizationUserNotFound] = "User not found.";

			this[TranslationKeys.AuthorizationEmailCannotBeNull] = "Email cannot be null.";
			this[TranslationKeys.AuthorizationEmailDoesNotMatchPattern] = "Email does not match pattern - somemail@gmail.com.";

			this[TranslationKeys.AuthorizationUsernameCannotBeNull] = "Username cannot be null.";
			this[TranslationKeys.AuthorizationUsernameDoesNotMatchLength] = "Username length must be between {0} and {1}.";

			this[TranslationKeys.AuthorizationLanguageNotFound] = "Specified language not found.";
		}
	}
}
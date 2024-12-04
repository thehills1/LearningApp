using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.Langs.Core;

namespace LearningApp.Service.Langs.API.Russian
{
	[Lang(Language.Russian)]
	public class Authorization : LangBase
	{
		public Authorization()
		{
			this[TranslationKeys.AuthorizationLoginRequestCanNotBeNull] = "Запрос авторизации не может быть null.";
			this[TranslationKeys.AuthorizationRegistrationRequestCanNotBeNull] = "Запрос регистрации аккаунта не может быть null.";

			this[TranslationKeys.AuthorizationRefreshTokenNotFound] = "Refresh токен не найден.";
			this[TranslationKeys.AuthorizationSessionIsNotValid] = "Сессия устарела.";
			this[TranslationKeys.AuthorizationUserNotFound] = "Пользователь не найден.";

			this[TranslationKeys.AuthorizationEmailCanNotBeNull] = "Электронная почта не может быть null.";
			this[TranslationKeys.AuthorizationEmailDoesNotMatchPattern] = "Электронная почта должна быть указана в формате - somemail@gmail.com.";

			this[TranslationKeys.AuthorizationUsernameCanNotBeNull] = "Имя пользователя не может быть null.";
			this[TranslationKeys.AuthorizationUsernameDoesNotMatchLength] = "Длина имени пользователя должна быть от {0} по {1} символов.";

			this[TranslationKeys.AuthorizationLanguageNotFound] = "Указанный язык не найден.";
		}
	}
}
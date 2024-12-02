using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.Langs.Core;

namespace LearningApp.Service.Langs.API.Russian
{
	[Lang(Language.Russian)]
	public class Authorization : LangBase
	{
		public Authorization()
		{
			this[TranslationKeys.AuthorizationLoginRequestCannotBeNull] = "Запрос авторизации не может быть null.";
			this[TranslationKeys.AuthorizationRegistrationRequestCannotBeNull] = "Запрос регистрации аккаунта не может быть null.";

			this[TranslationKeys.AuthorizationRefreshTokenNotFound] = "Refresh токен не найден.";
			this[TranslationKeys.AuthorizationSessionIsNotValid] = "Сессия устарела.";
			this[TranslationKeys.AuthorizationUserNotFound] = "Пользователь не найден.";

			this[TranslationKeys.AuthorizationEmailCannotBeNull] = "Электронная почта не может быть null.";
			this[TranslationKeys.AuthorizationEmailDoesNotMatchPattern] = "Электронная почта должна быть указана в формате - somemail@gmail.com.";

			this[TranslationKeys.AuthorizationUsernameCannotBeNull] = "Имя пользователя не может быть null.";
			this[TranslationKeys.AuthorizationUsernameDoesNotMatchLength] = "Длина имени пользователя должна быть от {0} по {1} символов.";

			this[TranslationKeys.AuthorizationPasswordCannotBeNull] = "Пароль не может быть null.";
			this[TranslationKeys.AuthorizationPasswordDoesNotMatchLength] = "Длина пароля должна быть от {0} по {1} символов.";
			this[TranslationKeys.AuthorizationPasswordIsWrong] = "Неверный пароль.";

			this[TranslationKeys.AuthorizationLanguageNotFound] = "Указанный язык не найден.";
		}
	}
}
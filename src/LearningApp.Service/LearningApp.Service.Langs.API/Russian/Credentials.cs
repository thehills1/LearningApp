using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.Langs.Core;

namespace LearningApp.Service.Langs.API.Russian
{
	[Lang(Language.Russian)]
	public class Credentials : LangBase
	{
		public Credentials()
		{
			this[TranslationKeys.CredentialsPasswordCannotBeNull] = "Пароль не может быть null.";
			this[TranslationKeys.CredentialsPasswordDoesNotMatchLength] = "Длина пароля должна быть от {0} по {1} символов.";
			this[TranslationKeys.CredentialsNewPasswordCannotBeNull] = "Новый пароль не может быть null.";
			this[TranslationKeys.CredentialsNewPasswordDoesNotMatchLength] = "Длина нового пароля должна быть от {0} по {1} символов.";
			this[TranslationKeys.CredentialsPasswordIsWrong] = "Неверный пароль.";
		}
	}
}
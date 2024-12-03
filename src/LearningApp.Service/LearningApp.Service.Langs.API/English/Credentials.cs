using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.Langs.Core;

namespace LearningApp.Service.Langs.API.English
{
	[Lang(Language.English)]
	public class Credentials : LangBase
	{
		public Credentials()
		{
			this[TranslationKeys.CredentialsPasswordCannotBeNull] = "Password cannot be null.";
			this[TranslationKeys.CredentialsPasswordDoesNotMatchLength] = "Password length must be between {0} and {1}.";
			this[TranslationKeys.CredentialsNewPasswordCannotBeNull] = "New password cannot be null.";
			this[TranslationKeys.CredentialsNewPasswordDoesNotMatchLength] = "New password length must be between {0} and {1}.";
			this[TranslationKeys.CredentialsPasswordIsWrong] = "Wrong password.";
		}
	}
}
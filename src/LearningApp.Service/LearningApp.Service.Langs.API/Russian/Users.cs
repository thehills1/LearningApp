﻿using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.Langs.Core;

namespace LearningApp.Service.Langs.API.Russian
{
	[Lang(Language.Russian)]
	public class Users : LangBase
	{
		public Users()
		{
			this[TranslationKeys.UsersEmailCannotBeNull] = "Электронная почта не может быть null.";
			this[TranslationKeys.UsersUserWithSameEmailAlreadyExists] = "Пользователь с электронной почтой {0} уже существует.";
			this[TranslationKeys.UsersUserWithSameEmailNotExists] = "Пользователь с электронной почтой {0} не существует.";

			this[TranslationKeys.UsersUsernameCannotBeNull] = "Имя пользователя не может быть null.";
			this[TranslationKeys.UsersUserWithSameUsernameAlreadyExists] = "Пользователь с именем {0} уже существует.";
			this[TranslationKeys.UsersUserWithSameUsernameNotExists] = "Пользователь с именем {0} не существует.";
		}
	}
}
﻿using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.Langs.Core;

namespace LearningApp.Service.Langs.API.English
{
	[Lang(Language.English)]
	public class Users : LangBase
	{
		public Users()
		{
			this[TranslationKeys.UsersEmailCannotBeNull] = "Email cannot be null.";
			this[TranslationKeys.UsersUserWithSameEmailAlreadyExists] = "User with email {0} already exists.";
			this[TranslationKeys.UsersUserWithSameEmailNotExists] = "User with email {0} not exists.";

			this[TranslationKeys.UsersUsernameCannotBeNull] = "Username cannot be null.";
			this[TranslationKeys.UsersUserWithSameUsernameAlreadyExists] = "User with username {0} already exists.";
			this[TranslationKeys.UsersUserWithSameUsernameNotExists] = "User with username {0} not exists.";

			this[TranslationKeys.UsersUserIdCannotBeLessOrEqualToZero] = "User's id can't be less or equal to zero.";
			this[TranslationKeys.UsersUserNotFound] = "User not found.";

			this[TranslationKeys.UsersChangePasswordRequestCannotBeNull] = "Change password request cannot be null.";
		}
	}
}
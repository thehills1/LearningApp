using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.Langs.Core;

namespace LearningApp.Service.Langs.API.English
{
	[Lang(Language.English)]
	public class Common : LangBase
	{
		public Common()
		{
			this[TranslationKeys.CommonYouHasNoPerms] = "You has no perms to do this action.";
		}
	}
}
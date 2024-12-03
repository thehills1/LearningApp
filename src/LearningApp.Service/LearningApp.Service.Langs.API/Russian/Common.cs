using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.Langs.Core;

namespace LearningApp.Service.Langs.API.Russian
{
	[Lang(Language.Russian)]
	public class Common : LangBase
	{
		public Common()
		{
			this[TranslationKeys.CommonYouHasNoPerms] = "У вас нет доступа для выполнения данного действия.";
		}
	}
}
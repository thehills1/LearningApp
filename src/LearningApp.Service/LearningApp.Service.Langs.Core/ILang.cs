namespace LearningApp.Service.Langs.Core
{
	public interface ILang
	{
		string GetTranslation(string translationKey, params object[] args);
	}
}
using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.Langs.Core;

namespace LearningApp.Service.Langs.API.Russian
{
	[Lang(Language.Russian)]
	public class Questions : LangBase
	{
		public Questions() 
		{
			this[TranslationKeys.QuestionsSubmitRequestCanNotBeNull] = "Ответ на вопрос не может быть null.";
			this[TranslationKeys.QuestionsSubmitAnswersAreNotSpecified] = "Не указаны ответы на вопрос.";
			this[TranslationKeys.QuestionsQuestionIdCanNotBeLessOrEqualToZero] = "Идентификатор вопроса не может быть меньше или равен нулю.";
			this[TranslationKeys.QuestionsQuestionNotFound] = "Вопрос не найден.";
			this[TranslationKeys.QuestionsAnswerIdCanNotBeLessOrEqualToZero] = "Идентификатор ответа на вопрос не может быть меньше или равен нулю.";
			this[TranslationKeys.QuestionsAnswerNotFound] = "Ответ на вопрос не найден.";
			this[TranslationKeys.QuestionsAnswerIncorrectForQuestion] = "Указан недействительный ответ на вопрос.";
			this[TranslationKeys.QuestionsQuestionDifficultyNotDefined] = "Указан несуществующий тип сложности вопроса.";
			this[TranslationKeys.QuestionsSubmitActionNotDefined] = "Указан несуществующий тип действия ответа на вопрос.";
		}
	}
}
using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.Langs.Core;

namespace LearningApp.Service.Langs.API.English
{
	[Lang(Language.English)]
	public class Questions : LangBase
	{
		public Questions()
		{
			this[TranslationKeys.QuestionsSubmitRequestCanNotBeNull] = "Submit question answer request can't be null.";
			this[TranslationKeys.QuestionsSubmitAnswersAreNotSpecified] = "Question answers are not specified.";
			this[TranslationKeys.QuestionsQuestionIdCanNotBeLessOrEqualToZero] = "Question's id can't be less or equal to zero.";
			this[TranslationKeys.QuestionsQuestionNotFound] = "Question not found.";
			this[TranslationKeys.QuestionsAnswerIdCanNotBeLessOrEqualToZero] = "Answer's id can't be less or equal to zero.";
			this[TranslationKeys.QuestionsAnswerNotFound] = "Question answer not found.";
			this[TranslationKeys.QuestionsAnswerIncorrectForQuestion] = "Incorrect question answer specified.";
			this[TranslationKeys.QuestionsQuestionDifficultyNotDefined] = "Specified question difficulty not defined.";
			this[TranslationKeys.QuestionsSubmitActionNotDefined] = "Specified submit action not defined.";
			this[TranslationKeys.QuestionsGetNextQuestionRequestCanNotBeNull] = "Get next question request can't be null.";
			this[TranslationKeys.QuestionsProgrammingLanguageNotDefined] = "Specified programming language not defined.";
		}
	}
}
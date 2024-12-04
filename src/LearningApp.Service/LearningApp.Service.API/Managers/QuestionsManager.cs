using System;
using System.Collections.Generic;
using System.Linq;
using LearningApp.Service.API.Contracts.Questions.Common;
using LearningApp.Service.API.Contracts.Questions.Requests;
using LearningApp.Service.API.Contracts.Questions.Responses;
using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.API.Entities;
using LearningApp.Service.API.Utils;
using LearningApp.Service.Core.Extensions;
using LearningApp.Service.Core.Syncs;
using LearningApp.Service.Database.Repositories;
using LearningApp.Service.Database.Tables;
using LearningApp.Service.Langs.API;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LearningApp.Service.API.Managers
{
	public class QuestionsManager : IQuestionsManager
	{
		private const int MinDaysToRepeatQuestion = 7;

		private readonly ILogger<QuestionsManager> _logger;
		private readonly IUserSessionsManager _userSessionsManager;
		private readonly ISyncManager _syncManager;
		private readonly IDbRepository _dbRepository;
		private readonly IImagesCache _imagesCache;
		private readonly Random _random;		

		public QuestionsManager(ILogger<QuestionsManager> logger, IUserSessionsManager userSessionsManager, ISyncManager syncManager, IDbRepository dbRepository, IImagesCache imagesCache, Random random)
		{
			_logger = logger;
			_userSessionsManager = userSessionsManager;
			_syncManager = syncManager;
			_dbRepository = dbRepository;
			_imagesCache = imagesCache;
			_random = random;
		}

		public MethodResult<QuestionInfoResponse> TryGetNextQuestion(long userId, QuestionDifficulty difficulty, Language language)
		{
			if (!CheckQuestionDifficulty(difficulty, out var checkDifficultyResult))
			{
				return checkDifficultyResult.ToResult<QuestionInfoResponse>();
			}

			return GetNextQuestionInternal(userId, difficulty, language);
		}

		public MethodResult<QuestionInfoResponse> TryGetQuestionById(long userId, PermissionLevel userPerms, long questionId, Language language)
		{
			if (!CheckQuestionId(questionId, out var checkQuestionIdResult, true))
			{
				return checkQuestionIdResult.ToResult<QuestionInfoResponse>();
			}

			var userSession = _userSessionsManager.GetOrAdd(userId);
			if (!userSession.IsAnswering(questionId) && userPerms < PermissionLevel.Admin)
			{
				return MethodResult<QuestionInfoResponse>.Error(StatusCodes.Status403Forbidden, TranslationKeys.CommonYouHasNoPerms);
			}

			return MethodResult<QuestionInfoResponse>.Success(MapQuestionToResponse(checkQuestionIdResult.Value, language));
		}

		public MethodResult<SubmitQuestionAnswerResponse> TrySubmitQuestionAnswer(long userId, SubmitQuestionAnswerRequest submitRequest)
		{
			if (submitRequest == null)
			{
				return MethodResult<SubmitQuestionAnswerResponse>.Error(StatusCodes.Status400BadRequest, TranslationKeys.QuestionsSubmitRequestCanNotBeNull);
			}

			if (!CheckQuestionId(submitRequest.QuestionId, out var checkQuestionIdResult, true))
			{
				return checkQuestionIdResult.ToResult<SubmitQuestionAnswerResponse>();
			}

			if (!CheckSubmitAction(submitRequest.SubmitAction, out var checkSubmitActionResult))
			{
				return checkSubmitActionResult.ToResult<SubmitQuestionAnswerResponse>();
			}

			var question = checkQuestionIdResult.Value;
			var answers = new List<Answer>();
			if (submitRequest.SubmitAction != SubmitAction.Cancel)
			{
				if (submitRequest.AnswerIds == null || submitRequest.AnswerIds.Count == 0)
				{
					return MethodResult<SubmitQuestionAnswerResponse>.Error(StatusCodes.Status400BadRequest, TranslationKeys.QuestionsSubmitAnswersAreNotSpecified);
				}

				foreach (var answerId in submitRequest.AnswerIds)
				{
					if (!CheckAnswerId(question.Id, answerId, out var checkAnswerIdResult, true))
					{
						return checkAnswerIdResult.ToResult<SubmitQuestionAnswerResponse>();
					}

					answers.Add(checkAnswerIdResult.Value);
				}
			}

			var userSession = _userSessionsManager.GetOrAdd(userId);
			if (!userSession.IsAnswering(submitRequest.QuestionId))
			{
				return MethodResult<SubmitQuestionAnswerResponse>.Error(StatusCodes.Status403Forbidden, TranslationKeys.CommonYouHasNoPerms);
			}

			return SubmitQuestionAnswerInternal(userId, submitRequest, question, answers, userSession);
		}

		private MethodResult<QuestionInfoResponse> GetNextQuestionInternal(long userId, QuestionDifficulty difficulty, Language language)
		{
			using (_syncManager.Lock(DefaultSyncs.GetNextQuestion(userId)))
			{
				var minDateToRepeatQuestion = DateTimeOffset.UtcNow.AddDays(-MinDaysToRepeatQuestion);
				var recentQuestionIds = _dbRepository.GetAll<Submission>(true)
					.Where(s => s.UserId == userId && s.SubmissionDate >= minDateToRepeatQuestion)
					.Join(_dbRepository.GetAll<Answer>(true),
						  submission => submission.AnswerId,
						  answer => answer.Id,
						  (submission, answer) => answer.QuestionId)
					.Distinct()
					.ToList();

				var userSession = _userSessionsManager.GetOrAdd(userId);
				var excludedQuestionIds = new HashSet<long>(recentQuestionIds.Concat(userSession.AnsweringQuestions));
				var unansweredQuestions = _dbRepository.GetAll<Question>(true).Where(q => q.Difficulty == difficulty && !excludedQuestionIds.Contains(q.Id)).ToList();
				if (unansweredQuestions.Count == 0)
				{
					return MethodResult<QuestionInfoResponse>.Success(default, StatusCodes.Status204NoContent);
				}

				var question = _random.NextElement(unansweredQuestions);
				userSession.AddAnsweringQuestion(question.Id);

				return MethodResult<QuestionInfoResponse>.Success(MapQuestionToResponse(question, language));
			}
		}

		private MethodResult<SubmitQuestionAnswerResponse> SubmitQuestionAnswerInternal(long userId, SubmitQuestionAnswerRequest submitRequest, Question question, List<Answer> answers, UserSession userSession)
		{
			using (_syncManager.Lock(DefaultSyncs.SubmitQuestionAnswer(userId, question.Id)))
			{
				userSession.RemoveAnsweringQuestion(question.Id);
				if (submitRequest.SubmitAction == SubmitAction.Cancel)
				{
					return MethodResult<SubmitQuestionAnswerResponse>.Success(default, StatusCodes.Status204NoContent);
				}

				var totalScore = 0D;
				var maxScore = question.MaxScore;
				foreach (var answer in answers)
				{
					var answerReceivedScore = answer.IsCorrect ? maxScore / answers.Count : 0D;
					totalScore += answerReceivedScore;

					var submission = new Submission()
					{
						UserId = userId,
						AnswerId = answer.Id,
						ReceivedScore = answerReceivedScore
					};

					_dbRepository.Add(submission);
				}

				var submitStatus = SubmitAnswerStatus.Wrong;
				if (totalScore > 0D)
				{
					if (totalScore == maxScore)
					{
						submitStatus = SubmitAnswerStatus.Correct;
					}
					else
					{
						submitStatus = SubmitAnswerStatus.PartiallyCorrect;
					}
				}

				var response = new SubmitQuestionAnswerResponse
				{
					Status = submitStatus,
					ReceivedScore = totalScore
				};

				return MethodResult<SubmitQuestionAnswerResponse>.Success(response);
			}
		}

		private bool CheckQuestionId(long questionId, out MethodResult<Question> checkResult, bool noTracking = true)
		{
			if (questionId <= 0)
			{
				checkResult = MethodResult<Question>.Error(StatusCodes.Status400BadRequest, TranslationKeys.QuestionsQuestionIdCanNotBeLessOrEqualToZero);
				return false;
			}

			var question = _dbRepository.Get<Question>(questionId, true);
			if (question == null)
			{
				checkResult = MethodResult<Question>.Error(StatusCodes.Status404NotFound, TranslationKeys.QuestionsQuestionNotFound);
				return false;
			}

			checkResult = MethodResult<Question>.Success(question);
			return true;
		}

		private bool CheckAnswerId(long questionId, long answerId, out MethodResult<Answer> checkResult, bool noTracking = true)
		{
			if (answerId <= 0)
			{
				checkResult = MethodResult<Answer>.Error(StatusCodes.Status400BadRequest, TranslationKeys.QuestionsAnswerIdCanNotBeLessOrEqualToZero);
				return false;
			}

			var answer = _dbRepository.Get<Answer>(answerId, noTracking);
			if (answer == null)
			{
				checkResult = MethodResult<Answer>.Error(StatusCodes.Status404NotFound, TranslationKeys.QuestionsAnswerNotFound);
				return false;
			}

			if (answer.QuestionId != questionId)
			{
				checkResult = MethodResult<Answer>.Error(StatusCodes.Status400BadRequest, TranslationKeys.QuestionsAnswerIncorrectForQuestion);
				return false;
			}

			checkResult = MethodResult<Answer>.Success(answer);
			return true;
		}

		private bool CheckQuestionDifficulty(QuestionDifficulty difficulty, out MethodResult checkResult)
		{
			if (!Enum.IsDefined(difficulty))
			{
				checkResult = MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.QuestionsQuestionDifficultyNotDefined);
				return false;
			}

			checkResult = MethodResult.Success();
			return true;
		}

		private bool CheckSubmitAction(SubmitAction submitAction, out MethodResult checkResult)
		{
			if (!Enum.IsDefined(submitAction))
			{
				checkResult = MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.QuestionsSubmitActionNotDefined);
				return false;
			}

			checkResult = MethodResult.Success();
			return true;
		}

		private QuestionInfoResponse MapQuestionToResponse(Question question, Language language)
		{
			var answers = GetAnswers(question.Id);
			var multipleCorrectAnswers = answers.Count(a => a.IsCorrect) > 1;
			var questionAnswerInfos = answers
				.Select(a => new AnswerInfo()
				{
					Id = a.Id,
					Title = GetTitleByLanguage(a.TitleId, language),
					ImageBase64 = a.ImageId.HasValue ? _imagesCache.GetImage(a.ImageId.Value).Base64 : null
				})
				.ToList();

			var response = new QuestionInfoResponse()
			{
				Id = question.Id,
				Difficulty = question.Difficulty,
				MaxScore = question.MaxScore,
				Title = GetTitleByLanguage(question.TitleId, language),
				ImageBase64 = question.ImageId.HasValue ? _imagesCache.GetImage(question.ImageId.Value).Base64 : null,
				Answers = questionAnswerInfos,
				MultipleCorrectAnswers = multipleCorrectAnswers
			};

			return response;
		}

		private string GetTitleByLanguage(long titleId, Language language)
		{
			var title = _dbRepository.Get<Title>(titleId, true);
			return language switch
			{
				Language.Russian => title.Russian,
				Language.English => title.English
			};
		}

		private List<Answer> GetAnswers(long questionId)
		{
			return _dbRepository.Get<Answer>(a => a.QuestionId == questionId, true).ToList();
		}
	}
}
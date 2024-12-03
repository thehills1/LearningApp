using LearningApp.Service.API.Contracts.Users.Common;
using LearningApp.Service.Langs.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.Service.API.Entities
{
	public class MethodResult<T> : MethodResult
	{
		public T Value { get; set; }

		public override IActionResult ToActionResult(Language language)
		{
			var result = base.ToActionResult(language);
			if (!IsSuccess) return result;
			
			var okObjectResult = result as OkObjectResult;
			okObjectResult.Value = Value;

			return okObjectResult;
		}

		public static MethodResult<T> Success(T result)
		{
			return new MethodResult<T>()
			{
				IsSuccess = true,
				StatusCode = StatusCodes.Status200OK,
				Value = result
			};
		}

		public static new MethodResult<T> Error(int statusCode, string message = null, params object[] translationKeyArgs)
		{
			return new MethodResult<T>()
			{
				IsSuccess = false,
				StatusCode = statusCode,
				Message = message,
				TranslationKeyArgs = translationKeyArgs
			};
		}
	}

	public class MethodResult
	{
		public bool IsSuccess { get; set; }

		public int StatusCode { get; set; }

		public string Message { get; set; }

		public object[] TranslationKeyArgs { get; set; }

		public virtual IActionResult ToActionResult(Language language)
		{
			if (IsSuccess)
			{
				return new OkObjectResult(null);
			}
			else
			{
				return new ObjectResult(Message.Translate(language, TranslationKeyArgs))
				{
					StatusCode = StatusCode
				};
			}
		}

		public MethodResult<T> ToResult<T>()
		{
			return new MethodResult<T>
			{
				IsSuccess = IsSuccess,
				StatusCode = StatusCode,
				Message = Message,
				TranslationKeyArgs = TranslationKeyArgs
			};
		}

		public static MethodResult Success()
		{
			return new MethodResult()
			{
				IsSuccess = true,
				StatusCode = StatusCodes.Status200OK
			};
		}

		public static MethodResult Error(int statusCode, string message = null, params object[] translationKeyArgs)
		{
			return new MethodResult()
			{
				IsSuccess = false,
				StatusCode = statusCode,
				Message = message,
				TranslationKeyArgs = translationKeyArgs
			};
		}
	}
}

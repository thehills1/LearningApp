using LearningApp.Service.API.Entities;
using LearningApp.Service.API.Utils;
using LearningApp.Service.Database.Repositories;
using LearningApp.Service.Database.Tables;
using LearningApp.Service.Langs.API;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LearningApp.Service.API.Managers
{
	public class CredentialsManager : ICredentialsManager
	{
		private const int MinPasswordLength = 8;
		private const int MaxPasswordLength = 32;

		private readonly ILogger<CredentialsManager> _logger;
		private readonly IDbRepository _dbRepository;

		public CredentialsManager(ILogger<CredentialsManager> logger, IDbRepository dbRepository)
		{
			_logger = logger;
			_dbRepository = dbRepository;
		}

		public void ChangePassword(User user, string password)
		{
			user.Password = PasswordHelper.HashPassword(password);
			_dbRepository.SaveChanges();
		}

		public bool CheckPasswordSyntax(string password, out MethodResult checkResult, bool newPassword = false)
		{
			if (password == null)
			{
				if (newPassword)
				{
					checkResult = MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.CredentialsNewPasswordCanNotBeNull);
				}
				else
				{
					checkResult = MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.CredentialsPasswordCanNotBeNull);
				}

				return false;
			}

			var passwordLength = password.Length;
			if (passwordLength < MinPasswordLength || passwordLength > MaxPasswordLength)
			{
				if (newPassword)
				{
					checkResult = MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.CredentialsNewPasswordDoesNotMatchLength, MinPasswordLength, MaxPasswordLength);
				}
				else
				{
					checkResult = MethodResult.Error(StatusCodes.Status400BadRequest, TranslationKeys.CredentialsPasswordDoesNotMatchLength, MinPasswordLength, MaxPasswordLength);
				}
				
				return false;
			}

			checkResult = MethodResult.Success();
			return true;
		}

		public bool CheckPassword(User user, string password)
		{
			var hashedPassword = PasswordHelper.HashPassword(password);
			if (user.Password != hashedPassword)
			{
				return false;
			}

			return true;
		}
	}
}
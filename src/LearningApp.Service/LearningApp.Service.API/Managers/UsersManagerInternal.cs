using System.Linq;
using LearningApp.Service.API.Entities;
using LearningApp.Service.Core.Extensions;
using LearningApp.Service.Database.Repositories;
using LearningApp.Service.Database.Tables;
using LearningApp.Service.Langs.API;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LearningApp.Service.API.Managers
{
	public class UsersManagerInternal : IUsersManagerInternal
	{
		private readonly ILogger<UsersManagerInternal> _logger;
		private readonly IDbRepository _dbRepository;

		public UsersManagerInternal(
			ILogger<UsersManagerInternal> logger,
			IDbRepository dbRepository)
		{
			_logger = logger;
			_dbRepository = dbRepository;
		}

		public MethodResult<User> CheckUserExistsByEmail(string email, bool errorIfExists, bool noTraking = true)
		{
			if (email.IsNullOrEmpty())
			{
				return MethodResult<User>.Error(StatusCodes.Status400BadRequest, TranslationKeys.UsersEmailCanNotBeNull);
			}

			var user = _dbRepository.Get<User>(t => t.Email == email, noTraking).FirstOrDefault();
			if (errorIfExists && user != null)
			{
				return MethodResult<User>.Error(StatusCodes.Status409Conflict, TranslationKeys.UsersUserWithSameEmailAlreadyExists, email);
			}

			if (!errorIfExists && user == null)
			{
				return MethodResult<User>.Error(StatusCodes.Status404NotFound, TranslationKeys.UsersUserWithSameEmailNotExists, email);
			}

			return MethodResult<User>.Success(user);
		}

		public MethodResult<User> CheckUserExistsByUsername(string username, bool errorIfExists, bool noTraking = true)
		{
			if (username.IsNullOrEmpty())
			{
				return MethodResult<User>.Error(StatusCodes.Status400BadRequest, TranslationKeys.UsersUsernameCanNotBeNull);
			}

			var user = _dbRepository.Get<User>(t => t.Username == username, noTraking).FirstOrDefault();
			if (errorIfExists && user != null)
			{
				return MethodResult<User>.Error(StatusCodes.Status409Conflict, TranslationKeys.UsersUserWithSameUsernameAlreadyExists, username);
			}

			if (!errorIfExists && user == null)
			{
				return MethodResult<User>.Error(StatusCodes.Status409Conflict, TranslationKeys.UsersUserWithSameUsernameNotExists, username);
			}

			return MethodResult<User>.Success(user);
		}

		public MethodResult<User> CheckUserExists(long id, bool noTracking = true)
		{
			if (id <= 0)
			{
				return MethodResult<User>.Error(StatusCodes.Status400BadRequest, TranslationKeys.UsersUserIdCanNotBeLessOrEqualToZero);
			}

			var user = GetUser(id);
			if (user == null)
			{
				return MethodResult<User>.Error(StatusCodes.Status404NotFound, TranslationKeys.UsersUserNotFound);
			}

			return MethodResult<User>.Success(user);
		}

		public User GetUser(long id, bool noTracking = true)
		{
			return _dbRepository.Get<User>(id, noTracking);
		}
	}
}
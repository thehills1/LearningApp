namespace LearningApp.Service.Langs.API
{
	public static class TranslationKeys
    {
		#region Authorization

		public const string AuthorizationLoginRequestCannotBeNull = "authorization.login.requestcannotbenull";
		public const string AuthorizationRegistrationRequestCannotBeNull = "authorization.registration.requestcannotbenull";

		public const string AuthorizationRefreshTokenNotFound = "authorization.refreshtoken.notfound";
		public const string AuthorizationSessionIsNotValid = "authorization.session.notvalid";
		public const string AuthorizationUserNotFound = "authorization.user.notfound";

		public const string AuthorizationEmailCannotBeNull = "authorization.email.cannotbenull";
		public const string AuthorizationEmailDoesNotMatchPattern = "authorization.email.doesnotmatchpattern";

		public const string AuthorizationUsernameCannotBeNull = "authorization.username.cannotbenull";
		/// <summary><code> Args = { (int)MinLength, (int)MaxLength } </code></summary>
		public const string AuthorizationUsernameDoesNotMatchLength = "authorization.username.doesnotmatchlength";

		public const string AuthorizationPasswordCannotBeNull = "authorization.password.cannotbenull";
		/// <summary><code> Args = { (int)MinLength, (int)MaxLength } </code></summary>
		public const string AuthorizationPasswordDoesNotMatchLength = "authorization.password.doesnotmatchlength";
		public const string AuthorizationPasswordIsWrong = "authorization.password.iswrong";

		public const string AuthorizationLanguageNotFound = "authorization.language.notfound";

		#endregion

		#region Users

		public const string UsersEmailCannotBeNull = "users.email.cannotbenull";
		/// <summary><code> Args = { (string)Email } </code></summary>
		public const string UsersUserWithSameEmailAlreadyExists = "users.userwithsameemail.alreadyexists";
		/// <summary><code> Args = { (string)Email } </code></summary>
		public const string UsersUserWithSameEmailNotExists = "users.userwithsameemail.notexists";

		public const string UsersUsernameCannotBeNull = "users.username.cannotbenull";
		/// <summary><code> Args = { (string)Username } </code></summary>
		public const string UsersUserWithSameUsernameAlreadyExists = "users.userwithsameusername.alreadyexists";
		/// <summary><code> Args = { (string)Username } </code></summary>
		public const string UsersUserWithSameUsernameNotExists = "users.userwithsameusername.notexists";

		#endregion
	}
}
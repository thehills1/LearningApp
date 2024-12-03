namespace LearningApp.Service.Langs.API
{
	public static class TranslationKeys
    {
		#region Common

		public const string CommonYouHasNoPerms = "common.youhavenoperms";

		#endregion

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

		public const string UsersUserIdCannotBeLessOrEqualToZero = "users.user.idcannotbelessorequaltozero";
		public const string UsersUserNotFound = "users.user.notfound";

		public const string UsersChangePasswordRequestCannotBeNull = "users.changepassword.requestcannotbenull";

		#endregion

		#region Credentials

		public const string CredentialsPasswordCannotBeNull = "credentials.password.cannotbenull";
		/// <summary><code> Args = { (int)MinLength, (int)MaxLength } </code></summary>
		public const string CredentialsPasswordDoesNotMatchLength = "credentials.password.doesnotmatchlength";
		public const string CredentialsNewPasswordCannotBeNull = "credentials.newpassword.cannotbenull";
		/// <summary><code> Args = { (int)MinLength, (int)MaxLength } </code></summary>
		public const string CredentialsNewPasswordDoesNotMatchLength = "credentials.newpassword.doesnotmatchlength";
		public const string CredentialsPasswordIsWrong = "credentials.password.iswrong";

		#endregion
	}
}
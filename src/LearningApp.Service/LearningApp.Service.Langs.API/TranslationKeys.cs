namespace LearningApp.Service.Langs.API
{
	public static class TranslationKeys
    {
		#region Common

		public const string CommonYouHasNoPerms = "common.youhavenoperms";

		#endregion

		#region Authorization

		public const string AuthorizationLoginRequestCanNotBeNull = "authorization.login.requestcannotbenull";
		public const string AuthorizationRegistrationRequestCanNotBeNull = "authorization.registration.requestcannotbenull";

		public const string AuthorizationRefreshTokenNotFound = "authorization.refreshtoken.notfound";
		public const string AuthorizationSessionIsNotValid = "authorization.session.notvalid";
		public const string AuthorizationUserNotFound = "authorization.user.notfound";

		public const string AuthorizationEmailCanNotBeNull = "authorization.email.cannotbenull";
		public const string AuthorizationEmailDoesNotMatchPattern = "authorization.email.doesnotmatchpattern";

		public const string AuthorizationUsernameCanNotBeNull = "authorization.username.cannotbenull";
		/// <summary><code> Args = { (int)MinLength, (int)MaxLength } </code></summary>
		public const string AuthorizationUsernameDoesNotMatchLength = "authorization.username.doesnotmatchlength";

		public const string AuthorizationLanguageNotFound = "authorization.language.notfound";

		#endregion

		#region Users

		public const string UsersEmailCanNotBeNull = "users.email.cannotbenull";
		/// <summary><code> Args = { (string)Email } </code></summary>
		public const string UsersUserWithSameEmailAlreadyExists = "users.userwithsameemail.alreadyexists";
		/// <summary><code> Args = { (string)Email } </code></summary>
		public const string UsersUserWithSameEmailNotExists = "users.userwithsameemail.notexists";

		public const string UsersUsernameCanNotBeNull = "users.username.cannotbenull";
		/// <summary><code> Args = { (string)Username } </code></summary>
		public const string UsersUserWithSameUsernameAlreadyExists = "users.userwithsameusername.alreadyexists";
		/// <summary><code> Args = { (string)Username } </code></summary>
		public const string UsersUserWithSameUsernameNotExists = "users.userwithsameusername.notexists";

		public const string UsersUserIdCanNotBeLessOrEqualToZero = "users.user.idcannotbelessorequaltozero";
		public const string UsersUserNotFound = "users.user.notfound";

		public const string UsersChangePasswordRequestCanNotBeNull = "users.changepassword.requestcannotbenull";

		#endregion

		#region Credentials

		public const string CredentialsPasswordCanNotBeNull = "credentials.password.cannotbenull";
		/// <summary><code> Args = { (int)MinLength, (int)MaxLength } </code></summary>
		public const string CredentialsPasswordDoesNotMatchLength = "credentials.password.doesnotmatchlength";
		public const string CredentialsNewPasswordCanNotBeNull = "credentials.newpassword.cannotbenull";
		/// <summary><code> Args = { (int)MinLength, (int)MaxLength } </code></summary>
		public const string CredentialsNewPasswordDoesNotMatchLength = "credentials.newpassword.doesnotmatchlength";
		public const string CredentialsPasswordIsWrong = "credentials.password.iswrong";

		#endregion

		#region Questions

		public const string QuestionsSubmitRequestCanNotBeNull = "questions.submit.requestcannotbenull";
		public const string QuestionsSubmitAnswersAreNotSpecified = "questions.submit.answersarenotspecified";
		public const string QuestionsQuestionIdCanNotBeLessOrEqualToZero = "questions.question.idcannotbelessorequaltozero";
		public const string QuestionsQuestionNotFound = "questions.question.notfound";
		public const string QuestionsAnswerIdCanNotBeLessOrEqualToZero = "questions.answer.idcannotbelessorequaltozero";
		public const string QuestionsAnswerNotFound = "questions.answer.notfound";
		public const string QuestionsAnswerIncorrectForQuestion = "questions.answer.incorrectforquestion";
		public const string QuestionsQuestionDifficultyNotDefined = "questions.difficulty.notdefined";
		public const string QuestionsSubmitActionNotDefined = "questions.difficulty.notdefined";

		#endregion
	}
}
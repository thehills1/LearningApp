package com.example.learningappandroid.ui.viewmodel

import androidx.databinding.BaseObservable
import androidx.databinding.Bindable
import com.example.learningappandroid.BR
import com.example.learningappandroid.api.contracts.authorization.requests.LoginRequest
import com.example.learningappandroid.api.contracts.users.common.Language
import com.example.learningappandroid.api.controllers.AuthorizationController
import com.example.learningappandroid.utils.APIUtils
import kotlinx.coroutines.*
import retrofit2.Retrofit

class LoginViewModel(
	private val retrofit: Retrofit
) : BaseObservable() {
	private val job = SupervisorJob()
	private val scope = CoroutineScope(Dispatchers.IO + job)

	@get:Bindable
	var email: String = ""
		set(value) {
			field = value
			notifyPropertyChanged(BR.email)
			validateInputs()
		}

	@get:Bindable
	var emailErrorMessage: String? = null
		private set(value) {
			field = value
			notifyPropertyChanged(BR.emailErrorMessage)
		}

	@get:Bindable
	var password: String = ""
		set(value) {
			field = value
			notifyPropertyChanged(BR.password)
			validateInputs()
		}

	@get:Bindable
	var passwordErrorMessage: String? = null
		private set(value) {
			field = value
			notifyPropertyChanged(BR.passwordErrorMessage)
		}

	@get:Bindable
	var errorMessage: String? = null
		private set(value) {
			field = value
			notifyPropertyChanged(BR.errorMessage)
		}

	@get:Bindable
	var loginButtonEnabled: Boolean = false
		private set(value) {
			field = value
			notifyPropertyChanged(BR.loginButtonEnabled)
		}

	@get:Bindable
	var authorizing: Boolean = false
		private set(value) {
			field = value
			loginButtonEnabled = !value
			notifyPropertyChanged(BR.authorizing)
		}

	fun tryAuthorize() {
		if (!loginButtonEnabled) return

		authorizing = true
		errorMessage = null

		scope.launch {
			val controller = AuthorizationController(retrofit)
			val response = controller.login(LoginRequest(email, password, Language.getIntValue(Language.Russian)))
			withContext(Dispatchers.IO) {
				if (response.isSuccessful) {
					val tokenResponse = response.body()

				} else {
					errorMessage = APIUtils.getErrorText(response)
					authorizing = false
				}
			}
		}
	}

	private fun validateInputs() {
		loginButtonEnabled = validateEmail() && validatePassword()
	}

	private fun validateEmail(): Boolean {
		val isEmailValid = android.util.Patterns.EMAIL_ADDRESS.matcher(email).matches()
		if (isEmailValid) {
			emailErrorMessage = null
		} else {
			emailErrorMessage = "Указана некорректная почта."
		}

		return isEmailValid
	}

	private fun validatePassword(): Boolean {
		val isPasswordValid = password.length >= 8
		if (isPasswordValid) {
			passwordErrorMessage = null
		} else {
			passwordErrorMessage = "Минимальная длина пароля - 8 символов."
		}

		return isPasswordValid
	}
}
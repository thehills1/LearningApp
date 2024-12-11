package com.example.learningappandroid.ui.viewmodel

import androidx.databinding.BaseObservable
import androidx.databinding.Bindable
import androidx.navigation.fragment.NavHostFragment.Companion.findNavController
import com.example.learningappandroid.BR
import com.example.learningappandroid.R
import com.example.learningappandroid.api.contracts.authorization.requests.RegistrationRequest
import com.example.learningappandroid.api.contracts.authorization.responses.AccessTokenResponse
import com.example.learningappandroid.api.contracts.users.common.Language
import com.example.learningappandroid.api.controllers.AuthorizationController
import com.example.learningappandroid.ui.view.fragments.RegistrationFragment
import com.example.learningappandroid.utils.APIUtils
import com.example.learningappandroid.utils.datastore.DataStoreManager
import kotlinx.coroutines.*

class RegistrationViewModel(
	private val fragment: RegistrationFragment
) : BaseObservable() {
	companion object {
		val minPasswordLength = 8;
		val maxPasswordLength = 32;
		private val minUsernameLength = 5;
		private val maxUsernameLength = 15;
	}

	private val job = SupervisorJob()
	private val scope = CoroutineScope(Dispatchers.IO + job)

	private val authorizationController = AuthorizationController.getInstance()

	@get:Bindable
	var username: String = ""
		set(value) {
			field = value
			notifyPropertyChanged(BR.username)
			validateInputs()
		}

	@get:Bindable
	var usernameErrorMessage: String? = null
		private set(value) {
			field = value
			notifyPropertyChanged(BR.usernameErrorMessage)
		}

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
	var repeatedPassword: String = ""
		set(value) {
			field = value
			notifyPropertyChanged(BR.repeatedPassword)
			validateInputs()
		}

	@get:Bindable
	var repeatedPasswordErrorMessage: String? = null
		private set(value) {
			field = value
			notifyPropertyChanged(BR.repeatedPasswordErrorMessage)
		}

	@get:Bindable
	var errorMessage: String? = null
		private set(value) {
			field = value
			notifyPropertyChanged(BR.errorMessage)
		}

	@get:Bindable
	var registerButtonEnabled: Boolean = false
		private set(value) {
			field = value
			notifyPropertyChanged(BR.registerButtonEnabled)
		}

	@get:Bindable
	var registering: Boolean = false
		private set(value) {
			field = value
			registerButtonEnabled = !value
			notifyPropertyChanged(BR.registering)
		}

	fun tryRegister() {
		if (!registerButtonEnabled) return

		registering = true
		errorMessage = null

		scope.launch {
			val response = authorizationController.register(RegistrationRequest(email, username, password, Language.getIntValue(Language.Russian)))
			withContext(Dispatchers.IO) {
				if (response.isSuccessful) {
					DataStoreManager.appendAccessTokenInfo(response.body() as AccessTokenResponse)
				} else {
					errorMessage = APIUtils.getErrorText(response)
					registering = false
				}
			}
		}
	}

	fun navigateToAuthorization() {
		findNavController(fragment).navigate(R.id.action_navigate_to_authorization)
	}

	private fun validateInputs() {
		registerButtonEnabled = validateUsername() && validateEmail() && validatePassword() && validateRepeatedPassword()
	}

	private fun validateUsername(): Boolean {
		val isUsernameValid = username.length in minUsernameLength..maxUsernameLength
		if (isUsernameValid) {
			usernameErrorMessage = null
		} else {
			usernameErrorMessage = "Имя пользователя должно иметь длину от 5 до 15 символов (включительно)."
		}

		return isUsernameValid
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
		val isPasswordValid = password.length in minPasswordLength..maxPasswordLength
		if (isPasswordValid) {
			passwordErrorMessage = null
		} else {
			passwordErrorMessage = "Длина пароля должна быть от 8 до 32 символов (включительно)."
		}

		return isPasswordValid
	}

	private fun validateRepeatedPassword(): Boolean {
		val isRepeatedPasswordValid = password == repeatedPassword
		if (isRepeatedPasswordValid) {
			repeatedPasswordErrorMessage = null
		} else {
			repeatedPasswordErrorMessage = "Введённые пароли не совпадают."
		}

		return isRepeatedPasswordValid
	}
}
package com.example.learningappandroid.ui.viewmodel

import androidx.databinding.BaseObservable
import androidx.databinding.Bindable
import androidx.navigation.fragment.NavHostFragment.Companion.findNavController
import com.example.learningappandroid.BR
import com.example.learningappandroid.R
import com.example.learningappandroid.api.contracts.authorization.requests.LoginRequest
import com.example.learningappandroid.api.contracts.authorization.responses.AccessTokenResponse
import com.example.learningappandroid.api.contracts.users.common.Language
import com.example.learningappandroid.api.controllers.AuthorizationController
import com.example.learningappandroid.ui.view.fragments.AuthorizationFragment
import com.example.learningappandroid.ui.viewmodel.RegistrationViewModel.Companion.maxPasswordLength
import com.example.learningappandroid.ui.viewmodel.RegistrationViewModel.Companion.minPasswordLength
import com.example.learningappandroid.utils.APIUtils
import com.example.learningappandroid.utils.datastore.DataStoreManager
import kotlinx.coroutines.*

class AuthorizationViewModel(
	private val fragment: AuthorizationFragment
) : BaseObservable() {
	private val job = SupervisorJob()
	private val scope = CoroutineScope(Dispatchers.IO + job)

	private val authorizationController = AuthorizationController.getInstance()

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
			val response = authorizationController.login(LoginRequest(email, password, Language.getIntValue(Language.Russian)))
			withContext(Dispatchers.Main) {
				if (response.isSuccessful) {
					DataStoreManager.appendAccessTokenInfo(response.body() as AccessTokenResponse)
					navigateToHome()
				} else {
					errorMessage = APIUtils.getErrorText(response)
					authorizing = false
				}
			}
		}
	}

	fun navigateToRegistration() {
		findNavController(fragment).navigate(R.id.action_navigate_to_registration)
	}

	fun navigateToHome() {
		findNavController(fragment).navigate(R.id.action_navigate_to_home)
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
		val isPasswordValid = password.length in minPasswordLength..maxPasswordLength
		if (isPasswordValid) {
			passwordErrorMessage = null
		} else {
			passwordErrorMessage = "Длина пароля должна быть от 8 до 32 символов (включительно)."
		}

		return isPasswordValid
	}
}
package com.example.learningappandroid.ui.fragments

import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.*
import androidx.fragment.app.Fragment
import com.example.learningappandroid.R

class LoginScreenFragment : Fragment() {

	private lateinit var emailEditText: EditText
	private lateinit var passwordEditText: EditText
	private lateinit var emailErrorTextView: TextView
	private lateinit var passwordErrorTextView: TextView
	private lateinit var loginButton: Button
	private lateinit var errorMessageTextView: TextView
	private lateinit var registerLinkTextView: TextView

	override fun onCreateView(
		inflater: LayoutInflater,
		container: ViewGroup?,
		savedInstanceState: Bundle?
	): View {
		return inflater.inflate(R.layout.login_fragment, container, false)
	}

	override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
		super.onViewCreated(view, savedInstanceState)

		emailEditText = view.findViewById(R.id.email)
		passwordEditText = view.findViewById(R.id.password)
		emailErrorTextView = view.findViewById(R.id.email_error)
		passwordErrorTextView = view.findViewById(R.id.password_error)
		loginButton = view.findViewById(R.id.login_button)
		errorMessageTextView = view.findViewById(R.id.error_message)
		registerLinkTextView = view.findViewById(R.id.register_link)

		setupUI()
	}

	private fun setupUI() {
		val textWatcher = object : TextWatcher {
			override fun beforeTextChanged(s: CharSequence?, start: Int, count: Int, after: Int) {}
			override fun onTextChanged(s: CharSequence?, start: Int, before: Int, count: Int) {
				validateInput()
			}
			override fun afterTextChanged(s: Editable?) {}
		}

		emailEditText.addTextChangedListener(textWatcher)
		passwordEditText.addTextChangedListener(textWatcher)

		loginButton.setOnClickListener {
			val email = emailEditText.text.toString()
			val password = passwordEditText.text.toString()
			// Ваш код для обработки логина
		}

		registerLinkTextView.setOnClickListener {
			// Ваш код для перехода к регистрации
		}
	}

	private fun validateInput() {
		val emailText = emailEditText.text?.toString()?.trim() ?: ""
		val passwordText = passwordEditText.text?.toString()?.trim() ?: ""

		val isEmailValid = android.util.Patterns.EMAIL_ADDRESS.matcher(emailText).matches()
		emailErrorTextView.visibility = if (isEmailValid) View.GONE else View.VISIBLE

		val isPasswordValid = passwordText.length >= 8
		passwordErrorTextView.visibility = if (isPasswordValid) View.GONE else View.VISIBLE

		loginButton.isEnabled = isEmailValid && isPasswordValid
	}
}
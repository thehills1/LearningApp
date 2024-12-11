package com.example.learningappandroid.api.contracts.authorization.requests

import com.example.learningappandroid.api.contracts.users.common.Language

data class RegistrationRequest(
	val email: String,
	val username: String,
	val password: String,
	val language: Language)
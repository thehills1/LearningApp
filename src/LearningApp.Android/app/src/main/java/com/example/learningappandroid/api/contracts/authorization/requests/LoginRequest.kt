package com.example.learningappandroid.api.contracts.authorization.requests

import com.example.learningappandroid.api.contracts.users.common.Language

data class LoginRequest(
	val email: String,
	val password: String,
	val language: Int
)
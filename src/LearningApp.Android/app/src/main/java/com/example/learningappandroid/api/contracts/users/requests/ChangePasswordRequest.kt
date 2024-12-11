package com.example.learningappandroid.api.contracts.users.requests

data class ChangePasswordRequest(
	val password: String,
	val newPassword: String
)
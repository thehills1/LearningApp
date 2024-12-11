package com.example.learningappandroid.api.contracts.authorization.requests

data class RegistrationRequest(
	val email: String,
	val username: String,
	val password: String,
	val language: Int)
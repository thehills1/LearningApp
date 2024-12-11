package com.example.learningappandroid.api.contracts.authorization.responses

data class AccessTokenResponse(
	val tokenType: String,
	val accessToken: String,
	val accessTokenExpireDate: String,
	val refreshToken: String,
	val refreshTokenExpireDate: String,
)
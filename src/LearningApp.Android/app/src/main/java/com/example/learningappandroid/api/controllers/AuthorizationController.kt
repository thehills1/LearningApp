package com.example.learningappandroid.api.controllers

import com.example.learningappandroid.api.contracts.authorization.IAuthorizationController
import com.example.learningappandroid.api.contracts.authorization.requests.LoginRequest
import com.example.learningappandroid.api.contracts.authorization.requests.RegistrationRequest
import com.example.learningappandroid.api.contracts.authorization.responses.AccessTokenResponse
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.http.Body
import retrofit2.http.Header

class AuthorizationController(retrofit: Retrofit) : IAuthorizationController {

	private val api: IAuthorizationController = retrofit.create(IAuthorizationController::class.java)

	override suspend fun login(@Body loginRequest: LoginRequest): Response<AccessTokenResponse> {
		return api.login(loginRequest)
	}

	override suspend fun register(@Body registrationRequest: RegistrationRequest): Response<AccessTokenResponse> {
		return api.register(registrationRequest)
	}

	override suspend fun refresh(@Header("Authorization") bearerToken: String): Response<AccessTokenResponse> {
		return api.refresh(bearerToken)
	}

	override suspend fun logout(@Header("Authorization") bearerToken: String): Response<Unit> {
		return api.logout(bearerToken)
	}
}
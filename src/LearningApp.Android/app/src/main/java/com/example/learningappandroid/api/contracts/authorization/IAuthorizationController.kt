package com.example.learningappandroid.api.contracts.authorization

import com.example.learningappandroid.api.contracts.authorization.requests.*
import com.example.learningappandroid.api.contracts.authorization.responses.AccessTokenResponse
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.Header
import retrofit2.http.POST

interface IAuthorizationController {

	/**
	 * Авторизоваться по почте и паролю.
	 * POST /authorization/login
	 */
	@POST("authorization/login")
	suspend fun login(@Body loginRequest: LoginRequest): Response<AccessTokenResponse>

	/**
	 * Зарегистрироваться по почте, имени пользователя, паролю и языку.
	 * POST /authorization/register
	 */
	@POST("authorization/register")
	suspend fun register(@Body registrationRequest: RegistrationRequest): Response<AccessTokenResponse>

	/**
	 * Обновить access токен по refresh токену.
	 * POST /authorization/refresh
	 * Требуется заголовок Authorization: Bearer <refresh_token>
	 */
	@POST("authorization/refresh")
	suspend fun refresh(@Header("Authorization") bearerToken: String): Response<AccessTokenResponse>

	/**
	 * Выйти из аккаунта.
	 * POST /authorization/logout
	 * Требуется заголовок Authorization: Bearer <access_token>
	 */
	@POST("authorization/logout")
	suspend fun logout(@Header("Authorization") bearerToken: String): Response<Unit>
}

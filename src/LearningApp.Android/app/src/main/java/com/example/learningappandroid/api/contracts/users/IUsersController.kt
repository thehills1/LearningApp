package com.example.learningappandroid.api.contracts.users

import com.example.learningappandroid.api.contracts.users.requests.ChangePasswordRequest
import com.example.learningappandroid.api.contracts.users.responses.UserInfoResponse
import retrofit2.Response
import retrofit2.http.*

interface IUsersController {
	/**
	 * Получить информацию о пользователе по его идентификатору.
	 * GET /users/{userId}
	 */
	@GET("users/{userId}")
	suspend fun getUserById(
		@Path("userId") userId: Long,
		@Header("Authorization") bearerToken: String
	): Response<UserInfoResponse>

	/**
	 * Получить карточку профиля текущего пользователя.
	 * GET /users/profile
	 */
	@GET("users/profile")
	suspend fun getProfile(
		@Header("Authorization") bearerToken: String
	): Response<UserInfoResponse>

	/**
	 * Обновить пароль от аккаунта.
	 * PUT /users/password
	 */
	@PUT("users/password")
	suspend fun changePassword(
		@Header("Authorization") bearerToken: String,
		@Body changeRequest: ChangePasswordRequest
	): Response<Unit>
}
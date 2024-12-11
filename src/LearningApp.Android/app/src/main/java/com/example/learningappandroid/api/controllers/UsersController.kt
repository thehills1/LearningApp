package com.example.learningappandroid.api.controllers

import com.example.learningappandroid.api.contracts.users.IUsersController
import com.example.learningappandroid.api.contracts.users.requests.ChangePasswordRequest
import com.example.learningappandroid.api.contracts.users.responses.UserInfoResponse
import com.example.learningappandroid.utils.RetrofitBuilder
import com.example.learningappandroid.utils.datastore.DataStoreManager
import retrofit2.Response
import retrofit2.Retrofit

class UsersController(retrofit: Retrofit) : IUsersController {
	companion object {
		fun getInstance(): UsersController {
			return UsersController(RetrofitBuilder.getInstance())
		}
	}

	private val api: IUsersController = retrofit.create(IUsersController::class.java)

	override suspend fun getUserById(
		userId: Long,
		bearerToken: String
	): Response<UserInfoResponse> {
		return api.getUserById(userId, bearerToken)
	}

	override suspend fun getProfile(bearerToken: String): Response<UserInfoResponse> {
		return api.getProfile(bearerToken)
	}

	override suspend fun changePassword(
		bearerToken: String,
		changeRequest: ChangePasswordRequest
	): Response<Unit> {
		return api.changePassword(bearerToken, changeRequest)
	}
}
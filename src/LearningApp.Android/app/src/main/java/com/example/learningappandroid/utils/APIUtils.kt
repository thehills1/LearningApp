package com.example.learningappandroid.utils

import org.json.JSONException
import org.json.JSONObject
import retrofit2.Response

object APIUtils {
	fun getErrorText(response: Response<*>): String {
		val errorBody = response.errorBody()
		val errorMessage = errorBody?.string()?.let { errorJson ->
			try {
				val jsonObject = JSONObject(errorJson)
				jsonObject.getString("text")
			} catch (e: JSONException) {
				errorJson
			}
		} ?: ""

		return errorMessage
	}
}
package com.example.learningappandroid.utils.datastore

import android.content.Context
import androidx.datastore.preferences.core.edit
import androidx.datastore.preferences.preferencesDataStore
import kotlinx.coroutines.flow.firstOrNull
import java.time.Clock
import java.time.OffsetDateTime

class DataStoreManager {
	private var context: Context? = null

	private var accessTokenValue: String? = null
	private var accessTokenExpireDateValue: OffsetDateTime? = null
	private var refreshTokenValue: String? = null
	private var refreshTokenExpireDateValue: OffsetDateTime? = null

	private val USER_PREFERENCES_NAME = "user_preferences"
	val Context.dataStore by preferencesDataStore(
		name = USER_PREFERENCES_NAME
	)

	fun initialize(context: Context) {
		this.context = context
	}

	suspend fun getAccessToken(): String {
		if (accessTokenValue != null) return accessTokenValue as String

		var tokenValue = context?.dataStore?.data?.firstOrNull()?.get(PreferencesKeys.accessTokenKey)
		if (tokenValue == null) return ""

		accessTokenValue = tokenValue
		return tokenValue as String
	}

	suspend fun getAccessTokenExpireDate() : OffsetDateTime {
		if (accessTokenExpireDateValue != null) return accessTokenExpireDateValue as OffsetDateTime

		val rawValue = context?.dataStore?.data?.firstOrNull()?.get(PreferencesKeys.accessTokenExpireDateKey)
		if (rawValue == null) return OffsetDateTime.now(Clock.systemUTC())

		var expireDate = OffsetDateTime.parse(rawValue)
		accessTokenExpireDateValue = expireDate
		return expireDate
	}

	suspend fun setAccessToken(value: String) {
		context?.dataStore?.edit { preferences ->
			preferences[PreferencesKeys.accessTokenKey] = value
			accessTokenValue = value
		}
	}

	suspend fun setAccessTokenExpireDate(value: String) {
		context?.dataStore?.edit { preferences ->
			preferences[PreferencesKeys.accessTokenExpireDateKey] = value
			accessTokenExpireDateValue = OffsetDateTime.parse(value)
		}
	}

	suspend fun getRefreshToken(): String {
		if (refreshTokenValue != null) return refreshTokenValue as String

		var tokenValue = context?.dataStore?.data?.firstOrNull()?.get(PreferencesKeys.refreshTokenKey)
		if (tokenValue == null) return ""

		refreshTokenValue = tokenValue
		return tokenValue
	}

	suspend fun getRefreshTokenExpireDate() : OffsetDateTime {
		if (refreshTokenExpireDateValue != null) return refreshTokenExpireDateValue as OffsetDateTime

		val rawValue = context?.dataStore?.data?.firstOrNull()?.get(PreferencesKeys.refreshTokenExpireDateKey)
		if (rawValue == null) return OffsetDateTime.now(Clock.systemUTC())

		var expireDate = OffsetDateTime.parse(rawValue)
		refreshTokenExpireDateValue = expireDate
		return expireDate
	}

	suspend fun setRefreshToken(value: String) {
		context?.dataStore?.edit { preferences ->
			preferences[PreferencesKeys.refreshTokenKey] = value
			refreshTokenValue = value
		}
	}

	suspend fun setRefreshTokenExpireDate(value: String) {
		context?.dataStore?.edit { preferences ->
			preferences[PreferencesKeys.refreshTokenExpireDateKey] = value
			refreshTokenExpireDateValue = OffsetDateTime.parse(value)
		}
	}
}
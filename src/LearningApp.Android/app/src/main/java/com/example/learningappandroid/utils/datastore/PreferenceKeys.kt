package com.example.learningappandroid.utils.datastore

import androidx.datastore.preferences.core.stringPreferencesKey

object PreferencesKeys {
	val accessTokenKey = stringPreferencesKey("accessTokenKey")
	val accessTokenExpireDateKey = stringPreferencesKey("accessTokenExpireDateKey")
	val refreshTokenKey = stringPreferencesKey("refreshTokenKey")
	val refreshTokenExpireDateKey = stringPreferencesKey("refreshTokenExpireDateKey")
}
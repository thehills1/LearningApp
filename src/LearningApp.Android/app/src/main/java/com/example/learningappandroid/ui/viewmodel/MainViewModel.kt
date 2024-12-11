package com.example.learningappandroid.ui.viewmodel

import android.content.Context
import androidx.databinding.BaseObservable
import com.example.learningappandroid.utils.datastore.DataStoreManager
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.SupervisorJob
import kotlinx.coroutines.launch
import java.time.Clock
import java.time.OffsetDateTime

class MainViewModel constructor(context: Context) : BaseObservable() {
	private val job = SupervisorJob()
	private val viewModelScope = CoroutineScope(Dispatchers.Main + job)

	private var _isAuthorized: Boolean = false

	fun IsAuthorized() : Boolean {
		viewModelScope.launch {
			val result = DataStoreManager.getAccessTokenExpireDate().isBefore(OffsetDateTime.now(Clock.systemUTC()))
			_isAuthorized = result
		}

		return _isAuthorized
	}
}
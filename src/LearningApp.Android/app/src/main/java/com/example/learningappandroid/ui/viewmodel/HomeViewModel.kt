package com.example.learningappandroid.ui.viewmodel

import androidx.databinding.BaseObservable
import androidx.databinding.Bindable
import com.example.learningappandroid.BR
import com.example.learningappandroid.api.contracts.users.responses.UserInfoResponse
import com.example.learningappandroid.api.controllers.UsersController
import com.example.learningappandroid.ui.view.fragments.HomeFragment
import com.example.learningappandroid.utils.datastore.DataStoreManager
import kotlinx.coroutines.*

class HomeViewModel(
	private val fragment: HomeFragment
) : BaseObservable() {
	private val job = SupervisorJob()
	private val scope = CoroutineScope(Dispatchers.IO + job)

	private val usersController = UsersController.getInstance()

	@get:Bindable
	var userInfo: UserInfoResponse? = null
		set(value) {
			field = value
			notifyPropertyChanged(BR.userInfo)
		}

	fun loadUserInfo(): Job {
		return scope.launch {
			val response = usersController.getProfile(DataStoreManager.getAccessToken())
			withContext(Dispatchers.Main) {
				if (response.isSuccessful) {
					userInfo = response.body()
				} else {

				}
			}
		}
	}
}
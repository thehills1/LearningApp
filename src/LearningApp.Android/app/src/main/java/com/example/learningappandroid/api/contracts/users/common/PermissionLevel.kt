package com.example.learningappandroid.api.contracts.users.common

enum class PermissionLevel {
	User,
	Admin;

	companion object {
		fun getIntValue(perms: PermissionLevel) : Int {
			when (perms) {
				User -> return 0
				Admin -> return 1
			}
		}
	}
}
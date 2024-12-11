package com.example.learningappandroid.api.contracts.users.responses

import com.example.learningappandroid.api.contracts.questions.common.LastSubmissionsInfo
import com.example.learningappandroid.api.contracts.users.common.Language
import com.example.learningappandroid.api.contracts.users.common.PermissionLevel

data class UserInfoResponse(
	val id: Long,
	val email: String,
	val username: String,
	val permissionLevel: PermissionLevel,
	val language: Language,
	val registrationDate: String,
	val lastSubmissionsInfo: LastSubmissionsInfo
)
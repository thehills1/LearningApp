package com.example.learningappandroid.api.contracts.questions.common

data class LastSubmissionsInfo(
	val lastDays: Int,
	val receivedScore: Double,
	val treeLevel: Int
)
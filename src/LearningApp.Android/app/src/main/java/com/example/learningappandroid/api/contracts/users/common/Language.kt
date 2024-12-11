package com.example.learningappandroid.api.contracts.users.common

enum class Language {
	Russian,
	English;

	companion object {
		fun getIntValue(lang: Language) : Int {
			when (lang) {
				Russian -> return 0
				English -> return 1
			}
		}
	}
}
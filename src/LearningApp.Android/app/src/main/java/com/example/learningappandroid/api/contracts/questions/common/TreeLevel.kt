package com.example.learningappandroid.api.contracts.questions.common

enum class TreeLevel {
	Died,
	Seed,
	Small,
	Medium,
	Large;

	companion object {
		fun getIntValue(treeLevel: TreeLevel) : Int {
			when (treeLevel) {
				Died -> return 0
				Seed -> return 1
				Small -> return 2
				Medium -> return 3
				Large -> return 4
			}
		}
	}
}
package com.example.learningappandroid.ui.view

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.databinding.DataBindingUtil
import androidx.navigation.fragment.NavHostFragment
import com.example.learningappandroid.R
import com.example.learningappandroid.databinding.MainActivityBinding
import com.example.learningappandroid.ui.viewmodel.MainViewModel
import com.example.learningappandroid.utils.datastore.DataStoreManager

class MainActivity : AppCompatActivity() {
	private lateinit var binding: MainActivityBinding
	private lateinit var viewModel: MainViewModel

	override fun onCreate(savedInstanceState: Bundle?) {
		super.onCreate(savedInstanceState)

		DataStoreManager.initialize(this)

		viewModel = MainViewModel(applicationContext)
		binding = DataBindingUtil.setContentView(this, R.layout.main_activity)
		binding.viewModel = viewModel
		binding.lifecycleOwner = this

		val navHostFragment = supportFragmentManager.findFragmentById(R.id.nav_host_fragment) as NavHostFragment
		val navController = navHostFragment.navController

		if (viewModel.IsAuthorized()) {
			navController.navigate(R.id.main_screen)
		}
	}
}
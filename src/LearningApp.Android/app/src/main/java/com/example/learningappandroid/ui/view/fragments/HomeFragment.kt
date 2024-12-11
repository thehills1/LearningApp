package com.example.learningappandroid.ui.view.fragments

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import com.example.learningappandroid.databinding.HomeFragmentBinding
import com.example.learningappandroid.ui.viewmodel.HomeViewModel
import kotlinx.coroutines.launch

class HomeFragment : Fragment() {
	private lateinit var binding: HomeFragmentBinding
	private lateinit var viewModel: HomeViewModel

	override fun onCreateView(
		inflater: LayoutInflater,
		container: ViewGroup?,
		savedInstanceState: Bundle?
	): View {
		viewModel = HomeViewModel(this)

		binding = HomeFragmentBinding.inflate(inflater, container, false)
		binding.viewModel = viewModel
		binding.lifecycleOwner = viewLifecycleOwner

		viewLifecycleOwner.lifecycleScope.launch {
			viewModel.loadUserInfo().join()
		}

		return binding.root
	}
}
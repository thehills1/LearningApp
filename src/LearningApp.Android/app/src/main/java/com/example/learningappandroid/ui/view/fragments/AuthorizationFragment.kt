package com.example.learningappandroid.ui.view.fragments

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.example.learningappandroid.databinding.AuthorizationFragmentBinding
import com.example.learningappandroid.ui.viewmodel.AuthorizationViewModel

class AuthorizationFragment : Fragment() {
	private lateinit var binding: AuthorizationFragmentBinding
	private lateinit var viewModel: AuthorizationViewModel

	override fun onCreateView(
		inflater: LayoutInflater,
		container: ViewGroup?,
		savedInstanceState: Bundle?
	): View {
		viewModel = AuthorizationViewModel(this)

		binding = AuthorizationFragmentBinding.inflate(inflater, container, false)
		binding.viewModel = viewModel
		binding.lifecycleOwner = viewLifecycleOwner

		return binding.root
	}
}

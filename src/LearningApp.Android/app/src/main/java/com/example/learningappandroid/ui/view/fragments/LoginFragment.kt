package com.example.learningappandroid.ui.view.fragments

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.example.learningappandroid.databinding.LoginFragmentBinding
import com.example.learningappandroid.ui.viewmodel.LoginViewModel
import com.example.learningappandroid.utils.RetrofitBuilder

class LoginFragment : Fragment() {
	private lateinit var binding: LoginFragmentBinding
	private lateinit var viewModel: LoginViewModel

	override fun onCreateView(
		inflater: LayoutInflater,
		container: ViewGroup?,
		savedInstanceState: Bundle?
	): View {
		viewModel = LoginViewModel(RetrofitBuilder.getInstance())

		binding = LoginFragmentBinding.inflate(inflater, container, false)
		binding.viewModel = viewModel
		binding.lifecycleOwner = viewLifecycleOwner

		return binding.root
	}
}

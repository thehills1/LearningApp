package com.example.learningappandroid.ui.view.fragments

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.example.learningappandroid.databinding.RegistrationFragmentBinding
import com.example.learningappandroid.ui.viewmodel.RegistrationViewModel

class RegistrationFragment : Fragment() {
	private lateinit var binding: RegistrationFragmentBinding
	private lateinit var viewModel: RegistrationViewModel

	override fun onCreateView(
		inflater: LayoutInflater,
		container: ViewGroup?,
		savedInstanceState: Bundle?
	): View {
		viewModel = RegistrationViewModel(this)

		binding = RegistrationFragmentBinding.inflate(inflater, container, false)
		binding.viewModel = viewModel
		binding.lifecycleOwner = viewLifecycleOwner

		return binding.root
	}
}
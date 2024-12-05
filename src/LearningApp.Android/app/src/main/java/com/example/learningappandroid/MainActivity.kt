package com.example.learningappandroid

import LoginScreen
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Surface
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import com.example.learningappandroid.ui.theme.LearningAppAndroidTheme

class MainActivity : ComponentActivity() {
    @OptIn(ExperimentalMaterial3Api::class)
	override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContent {
			Surface(
				modifier = Modifier.fillMaxSize()
			) {
				// Показать экран входа
				LoginScreen(
					onLoginClick = { /* Логика авторизации */ },
					onRegisterClick = { /* Логика регистрации */ }
				)
			}
        }
    }
}
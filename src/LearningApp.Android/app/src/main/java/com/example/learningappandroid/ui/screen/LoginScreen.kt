import androidx.compose.foundation.Image
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.Button
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.example.learningappandroid.R

@ExperimentalMaterial3Api
@Composable
fun LoginScreen(onLoginClick: () -> Unit, onRegisterClick: () -> Unit) {
	val email = remember { mutableStateOf("") }
	val password = remember { mutableStateOf("") }
	val isEmailValid = remember { mutableStateOf(false) }
	val isPasswordValid = remember { mutableStateOf(false) }
	val errorMessage = remember { mutableStateOf("") }

	fun validateEmail(email: String): Boolean {
		return android.util.Patterns.EMAIL_ADDRESS.matcher(email).matches()
	}

	fun validatePassword(password: String): Boolean {
		return password.length >= 8
	}

	Surface(
		modifier = Modifier.fillMaxSize(),
		color = Color(0xFF1E3D32)
	) {
		Column(
			modifier = Modifier
				.fillMaxSize()
				.padding(16.dp),
			verticalArrangement = Arrangement.Center,
			horizontalAlignment = Alignment.CenterHorizontally
		) {
			// Заголовок
			Text(
				text = "Вход в систему",
				fontSize = 24.sp,
				color = Color.White,
				modifier = Modifier.padding(bottom = 32.dp)
			)

			// Иконка
			Image(
				painter = painterResource(id = R.drawable.ic_login_image),
				contentDescription = "Login Illustration",
				modifier = Modifier
					.size(150.dp)
					.padding(bottom = 32.dp)
			)

			// Поле для ввода Email
			OutlinedTextField(
				value = email.value,
				onValueChange = {
					email.value = it
					isEmailValid.value = validateEmail(it)
				},
				label = { Text("Логин", color = Color.Gray) },
				placeholder = { Text("Укажите email", color = Color.Gray) },
				shape = RoundedCornerShape(size = 16.dp),
				isError = !isEmailValid.value && email.value.isNotEmpty(),
				modifier = Modifier
					.width(300.dp)
					.padding(bottom = 8.dp),
				colors = TextFieldDefaults.outlinedTextFieldColors(
					containerColor = Color.White,
					focusedBorderColor = if (isEmailValid.value) Color.Gray else Color.Red,
					unfocusedBorderColor = if (isEmailValid.value) Color.Gray else Color.Red
				)
			)
			if (!isEmailValid.value && email.value.isNotEmpty()) {
				Text(
					text = "Введите корректный email",
					color = Color.Red,
					fontSize = 12.sp,
					modifier = Modifier
						.padding(start = 16.dp, bottom = 8.dp)
						.align(Alignment.Start)
				)
			}

			// Поле для ввода Пароля
			OutlinedTextField(
				value = password.value,
				onValueChange = {
					password.value = it
					isPasswordValid.value = validatePassword(it)
				},
				label = { Text("Пароль", color = Color.Gray) },
				placeholder = { Text("Введите пароль", color = Color.Gray) },
				visualTransformation = PasswordVisualTransformation(),
				shape = RoundedCornerShape(size = 16.dp),
				isError = !isPasswordValid.value && password.value.isNotEmpty(),
				modifier = Modifier
					.width(300.dp)
					.padding(bottom = 8.dp),
				keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Password),
				colors = TextFieldDefaults.outlinedTextFieldColors(
					containerColor = Color.White,
					focusedBorderColor = if (isPasswordValid.value) Color.Gray else Color.Red,
					unfocusedBorderColor = if (isPasswordValid.value) Color.Gray else Color.Red
				)
			)
			if (!isPasswordValid.value && password.value.isNotEmpty()) {
				Text(
					text = "Пароль должен содержать минимум 6 символов",
					color = Color.Red,
					fontSize = 12.sp,
					modifier = Modifier
						.padding(start = 16.dp, bottom = 16.dp)
						.align(Alignment.Start)
				)
			}

			// Кнопка "Войти"
			Button(
				onClick = {
					if (isEmailValid.value && isPasswordValid.value) {
						onLoginClick()
					} else {
						errorMessage.value = "Некорректный email или пароль"
					}
				},
				shape = RoundedCornerShape(size = 16.dp),
				modifier = Modifier
					.width(300.dp)
					.padding(bottom = 16.dp),
				colors = ButtonDefaults.buttonColors(
					containerColor = if (isEmailValid.value && isPasswordValid.value) Color.White else Color.Gray,
					contentColor = Color.Black
				),
				enabled = isEmailValid.value && isPasswordValid.value
			) {
				Text(text = "Войти")
			}

			// Ошибка под кнопкой
			if (errorMessage.value.isNotEmpty()) {
				Text(
					text = errorMessage.value,
					color = Color.Red,
					fontSize = 14.sp,
					textAlign = TextAlign.Center,
					modifier = Modifier.padding(top = 8.dp)
				)
			}

			// Ссылка на регистрацию
			Text(
				text = "Еще не зарегистрированы?",
				color = Color.White,
				fontSize = 14.sp,
				modifier = Modifier.clickable { onRegisterClick() },
				textAlign = TextAlign.Center
			)
		}
	}
}


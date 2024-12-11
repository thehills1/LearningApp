plugins {
    alias(libs.plugins.android.application)
    alias(libs.plugins.kotlin.android)
    alias(libs.plugins.kotlin.compose)
	id ("kotlin-kapt")
}

android {
    namespace = "com.example.learningappandroid"
    compileSdk = 35

    defaultConfig {
        applicationId = "com.example.learningappandroid"
        minSdk = 31
        targetSdk = 34
        versionCode = 1
        versionName = "1.0"

        testInstrumentationRunner = "androidx.test.runner.AndroidJUnitRunner"
    }

    buildTypes {
        release {
            isMinifyEnabled = false
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
        }
    }

    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_11
        targetCompatibility = JavaVersion.VERSION_11
    }
    kotlinOptions {
        jvmTarget = "11"
    }

    buildFeatures {
        compose = true
		viewBinding = true
		dataBinding = true
    }

	dependencies {
		implementation(libs.androidx.core.ktx)
		implementation(libs.androidx.lifecycle.runtime.ktx)
		implementation(libs.androidx.activity.compose)
		implementation(platform(libs.androidx.compose.bom))
		implementation(libs.androidx.ui)
		implementation(libs.androidx.ui.graphics)
		implementation(libs.androidx.ui.tooling.preview)
		implementation(libs.androidx.material3)
		implementation(libs.androidx.navigation.fragment.ktx)
		implementation(libs.androidx.navigation.ui.ktx)
		implementation("androidx.fragment:fragment-ktx:1.8.5")
		implementation("androidx.constraintlayout:constraintlayout:2.1.4")
		implementation("androidx.appcompat:appcompat:1.3.0")
		implementation("com.google.android.material:material:1.4.0")
		implementation("androidx.constraintlayout:constraintlayout:2.0.4")
		implementation("androidx.lifecycle:lifecycle-livedata-ktx:2.3.1")
		implementation("androidx.lifecycle:lifecycle-viewmodel-ktx:2.3.1")
		implementation("com.squareup.retrofit2:retrofit:2.7.2")
		implementation("com.squareup.retrofit2:converter-gson:2.7.2")
		implementation("com.squareup.okhttp3:okhttp:3.6.0")
		implementation("androidx.datastore:datastore-preferences:1.0.0")
		implementation("androidx.datastore:datastore-preferences-rxjava2:1.0.0")
		implementation("androidx.datastore:datastore-preferences-rxjava3:1.0.0")
		testImplementation(libs.junit)
		androidTestImplementation(libs.androidx.junit)
		androidTestImplementation(libs.androidx.espresso.core)
		androidTestImplementation(platform(libs.androidx.compose.bom))
		androidTestImplementation(libs.androidx.ui.test.junit4)
		debugImplementation(libs.androidx.ui.tooling)
		debugImplementation(libs.androidx.ui.test.manifest)
	}
}
dependencies {
	implementation(libs.androidx.fragment)
}

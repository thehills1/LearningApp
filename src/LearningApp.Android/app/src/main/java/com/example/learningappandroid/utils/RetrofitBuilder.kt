package com.example.learningappandroid.utils

import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.security.SecureRandom
import java.security.cert.CertificateException
import java.security.cert.X509Certificate
import javax.net.ssl.SSLContext
import javax.net.ssl.SSLSocketFactory
import javax.net.ssl.TrustManager
import javax.net.ssl.X509TrustManager

object RetrofitBuilder {
	fun getInstance() : Retrofit {
		return Retrofit.Builder()
			.baseUrl("https://10.0.2.2:7171/api/")
			.client(getUnsafeOkHttpClient().build())
			.addConverterFactory(GsonConverterFactory.create())
			.build()
	}

	private fun getUnsafeOkHttpClient(): OkHttpClient.Builder =
		try {
			// Create a trust manager that does not validate certificate chains
			val trustAllCerts: Array<TrustManager> = arrayOf(
				object : X509TrustManager {
					@Throws(CertificateException::class)
					override fun checkClientTrusted(chain: Array<X509Certificate?>?,
													authType: String?) = Unit

					@Throws(CertificateException::class)
					override fun checkServerTrusted(chain: Array<X509Certificate?>?,
													authType: String?) = Unit

					override fun getAcceptedIssuers(): Array<X509Certificate> = arrayOf()
				}
			)
			// Install the all-trusting trust manager
			val sslContext: SSLContext = SSLContext.getInstance("SSL")
			sslContext.init(null, trustAllCerts, SecureRandom())
			// Create an ssl socket factory with our all-trusting manager
			val sslSocketFactory: SSLSocketFactory = sslContext.socketFactory
			val builder = OkHttpClient.Builder()
			builder.sslSocketFactory(sslSocketFactory,
				trustAllCerts[0] as X509TrustManager)
			builder.hostnameVerifier { _, _ -> true }
		} catch (e: Exception) {
			throw RuntimeException(e)
		}
}
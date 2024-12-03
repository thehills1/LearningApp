using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LearningApp.Service.API.Utils
{
	public static class PasswordHelper
	{
		private const string Seed = "cad76657865754655875aabdc66644578cca67557deee65564eeaa56768975cad71226";	

		/// <summary>
		/// Хэширует пароль.
		/// </summary>
		public static string HashPassword(string password)
		{
			password += Seed;

			var rnd = new Random(password.GetHashCode());
			var seed = new byte[10];
			rnd.NextBytes(seed);

			var inputBytes = Encoding.UTF8.GetBytes(password);

			var raw = new byte[seed.Length + inputBytes.Length];
			inputBytes.CopyTo(raw, 0);
			seed.CopyTo(raw, inputBytes.Length);

			for (var i = 0; i < raw.Length; i++)
			{
				var index = rnd.Next(raw.Length);
				var tmp = raw[i];
				raw[i] = raw[index];
				raw[index] = tmp;
			}

			using (var hasher = SHA512.Create())
			{
				var hash = hasher.ComputeHash(inputBytes);
				return string.Join(string.Empty, hash.Select(b => b.ToString("x2")));
			}
		}
	}
}
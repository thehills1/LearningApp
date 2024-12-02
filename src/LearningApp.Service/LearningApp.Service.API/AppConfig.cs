using LearningApp.Service.Core.Configs;

namespace LearningApp.Service.API
{
	public class AppConfig : ConfigBase<AppConfig>
	{
		public const string BasePath = "app_config.json";

		public string DatabaseConnectionString { get; set; }

		public Jwt Jwt { get; set; }
	}

	public class Jwt
	{
		public string Secret { get; set; }

		public int AccessTokenValidityDays { get; set; }

		public int RefreshTokenValidityDays { get; set; }
	}
}

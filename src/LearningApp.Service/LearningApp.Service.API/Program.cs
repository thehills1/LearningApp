using System;
using System.IO;
using System.Reflection;
using System.Text;
using LearningApp.Service.API.Managers;
using LearningApp.Service.API.Utils;
using LearningApp.Service.Core.Syncs;
using LearningApp.Service.Database;
using LearningApp.Service.Database.Repositories;
using LearningApp.Service.Langs.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace LearningApp.Service.API
{
	// TODO: политика авторизации по уровню доступа
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Logging.AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger());

			builder.Services.AddControllers();
			builder.Services.AddRouting(options => options.LowercaseUrls = true);

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Bearer",
					BearerFormat = "JWT",
					Scheme = "bearer",
					Description = "Specify the authorization token.",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});

				var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
			});
			
			var appConfig = AppConfig.Load(AppConfig.BasePath);
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidateAudience = false,
					ValidateIssuer = false,
					ClockSkew = TimeSpan.Zero,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfig.Jwt.Secret))
				};
			});

			SetupContainer(builder.Services, appConfig);

			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();

			LangManager.LoadLangs();

			app.Run();
		}

		private static void SetupContainer(IServiceCollection container, AppConfig appConfig)
		{
			container.AddSingleton(c => c);
			container.AddSingleton(appConfig);

			container.AddSingleton<IAccessTokenPool, AccessTokenPool>();
			container.AddSingleton<ISyncManager, SyncManager>();

			container.AddTransient<IAuthorizationManager, AuthorizationManager>();
			container.AddTransient<IUsersManager, UsersManager>();

			container.AddTransient<IDbRepository, DbRepository>();
			container.AddDbContext<DatabaseContext>(options =>
			{
				options.UseNpgsql(appConfig.DatabaseConnectionString, assembly => assembly.MigrationsAssembly("LearningApp.Service.Database.Migrations"));
			}, ServiceLifetime.Transient);
		}
	}
}
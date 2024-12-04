using System;
using LearningApp.Service.Database.Converters;
using LearningApp.Service.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace LearningApp.Service.Database
{
	public class DatabaseContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<Submission> Submissions { get; set; }

		public DbSet<Question> Questions { get; set; }

		public DbSet<Answer> Answers { get; set; }

		public DbSet<Title> Titles { get; set; }

		public DatabaseContext(DbContextOptions options) : base(options)
		{
			Database.EnsureCreated();
			Database.Migrate();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				foreach (var property in entityType.GetProperties())
				{
					if (property.ClrType == typeof(DateTimeOffset))
					{
						property.SetValueConverter(new DateTimeOffsetConverter());
					}
					else if (property.ClrType == typeof(DateTimeOffset?))
					{
						property.SetValueConverter(new NullableDateTimeOffsetConverter());
					}
				}
			}
		}
	}
}
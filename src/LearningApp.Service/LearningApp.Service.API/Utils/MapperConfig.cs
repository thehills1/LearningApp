using AutoMapper;
using LearningApp.Service.API.Contracts.Users.Responses;
using LearningApp.Service.Database.Tables;

namespace LearningApp.Service.API.Utils
{
	public static class MapperConfig
	{
		public static Mapper InitializeMapper()
		{
			var mapperConfiguration = new MapperConfiguration(c =>
			{
				c.CreateMap<User, UserInfoResponse>();
			});

			return new Mapper(mapperConfiguration);
		}
	}
}
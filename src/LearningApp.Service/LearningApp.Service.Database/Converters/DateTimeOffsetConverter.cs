using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LearningApp.Service.Database.Converters
{
	internal class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
	{
		public DateTimeOffsetConverter()
			: base(
				d => d.ToUniversalTime(),
				d => d.ToUniversalTime())
		{
		}
	}
}
using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LearningApp.Service.Database.Converters
{
	internal class NullableDateTimeOffsetConverter : ValueConverter<DateTimeOffset?, DateTimeOffset?>
	{
		public NullableDateTimeOffsetConverter()
			: base(
				d => d == null ? null : d.Value.ToUniversalTime(),
				d => d == null ? null : d.Value.ToUniversalTime())
		{
		}
	}
}
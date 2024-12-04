using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningApp.Service.Core.Extensions
{
	public static class RandomExtensions
	{
		public static T NextElement<T>(this Random random, IEnumerable<T> collection) where T : class
		{
			if (collection == null || !collection.Any()) return default;

			var elementsCount = collection.Count();
			var randomIndex = random.Next(0, elementsCount - 1);
			
			return collection.ElementAt(randomIndex);
		}
	}
}
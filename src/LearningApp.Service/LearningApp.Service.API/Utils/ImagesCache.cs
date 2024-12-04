using System.Collections.Concurrent;
using LearningApp.Service.Database.Repositories;
using LearningApp.Service.Database.Tables;

namespace LearningApp.Service.API.Utils
{
	public class ImagesCache : IImagesCache
	{
		private static ConcurrentDictionary<long, Image> _cache = new();

		private readonly IDbRepository _dbRepository;

		public ImagesCache(IDbRepository dbRepository) 
		{
			_dbRepository = dbRepository;
		}

		public Image GetImage(long id)
		{
			if (!_cache.TryGetValue(id, out var image))
			{
				image = _dbRepository.Get<Image>(id, true);
				if (image == null) return null;

				_cache.TryAdd(id, image);
			}

			return image;
		}

		public long SaveImage(Image image)
		{
			if (image == null) return 0;

			var id = _dbRepository.Add(image);
			_cache.TryAdd(id, image);

			return id;
		}

		public void RemoveImage(long id)
		{
			_dbRepository.Remove<Image>(id);
		}
	}
}
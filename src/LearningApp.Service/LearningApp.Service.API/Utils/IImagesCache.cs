using LearningApp.Service.Database.Tables;

namespace LearningApp.Service.API.Utils
{
	public interface IImagesCache
	{
		void RemoveImage(long id);
		Image GetImage(long id);
		long SaveImage(Image image);
	}
}
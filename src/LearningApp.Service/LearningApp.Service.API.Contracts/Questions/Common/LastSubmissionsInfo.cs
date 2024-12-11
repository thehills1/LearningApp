namespace LearningApp.Service.API.Contracts.Questions.Common
{
	public class LastSubmissionsInfo
	{
		public int LastDays { get; set; }

		public double ReceivedScore { get; set; }

		public TreeLevel TreeLevel { get; set; }
	}
}
namespace CrawlerDemo.Domain.Jobs
{
	public interface IJobHandler
	{
		bool ShouldHandle(IJob job);
		object ExecuteJob(IJob job);
	}
}
namespace CrawlerDemo.Domain.Jobs
{
	public interface IJobExecutorSettings
	{
		int MaxDegreeOfWorkers { get; set; }
	}
}
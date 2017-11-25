using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace CrawlerDemo.Domain.Jobs
{
	public interface IJobExecutor
	{
		void Start();
		void DispatchJob(IJob job);
		void Shutdown();
	}

	public class JobExecutor : IJobExecutor
	{
		private readonly IEnumerable<IJobHandler> _jobHandler;
		private readonly IJobExecutorSettings _jobExecutorSettings;
		private readonly BlockingCollection<IJob> _jobQueue = new BlockingCollection<IJob>(new ConcurrentQueue<IJob>(), 50);
		private readonly List<Task> _workersTasks = new List<Task>();
		private readonly List<JobConsumer> _jobConsumers = new List<JobConsumer>();
		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

		public JobExecutor(IEnumerable<IJobHandler> jobHandler, IJobExecutorSettings jobExecutorSettings)
		{
			_jobHandler = jobHandler;
			_jobExecutorSettings = jobExecutorSettings;
		}

		public void Start()
		{
			for (var i = 0; i < _jobExecutorSettings.MaxDegreeOfWorkers ; i++)
			{
				var jobConsumer = new JobConsumer(_jobQueue, _jobHandler, _cancellationTokenSource);
				_jobConsumers.Add(jobConsumer);
				var task = new Task(jobConsumer.ConsumeJobs, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning);
				task.Start();
				_workersTasks.Add(task);
			}
		}

		public void DispatchJob(IJob job)
		{
			_jobQueue.Add(job);
		}

		public void Shutdown()
		{
			_cancellationTokenSource.Cancel();
		}
	}
}
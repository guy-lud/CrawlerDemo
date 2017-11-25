using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CrawlerDemo.Domain.Jobs
{
	internal class JobConsumer : IJobConsumer
	{
		private readonly BlockingCollection<IJob> _collection;
		private readonly IEnumerable<IJobHandler> _jobHandlers;
		private readonly CancellationToken _cancellationToken; 

		public JobConsumer(BlockingCollection<IJob> collections,
			IEnumerable<IJobHandler> jobHandlers,
			CancellationTokenSource cancellationTokenSource)
		{
			_collection = collections;
			_jobHandlers = jobHandlers;
			_cancellationToken = cancellationTokenSource.Token;
		}

		public void ConsumeJobs()
		{
			while (!_cancellationToken.IsCancellationRequested)
			{
				try
				{
					var job = _collection.Take(_cancellationToken);
					_jobHandlers.FirstOrDefault(x => x.ShouldHandle(job))
						?.ExecuteJob(job);
				}
				catch (Exception exception)
				{
					
				}
			}
		}
	}
}
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MisskeyDriveSync
{
	public class ConcurrentTaskQueuing
	{
		public BlockingCollection<Task> TaskQueue { get; private set; } = new BlockingCollection<Task>(new ConcurrentQueue<Task>());

		private CancellationTokenSource Cancellation { get; set; }

		public bool IsRunning => (this.Cancellation != null);

		public Task Start(int concurrent = 1)
		{
			if (concurrent < 1)
				throw new ArgumentOutOfRangeException(nameof(concurrent));

			if (this.Cancellation != null)
				throw new InvalidOperationException("this TaskQueue is already started");

			return Task.Run(() =>
			{
				this.Cancellation = new CancellationTokenSource();

				void worker()
				{
					while (!this.Cancellation.IsCancellationRequested)
					{
						if (this.TaskQueue.TryTake(out Task t))
						{
							if (t != null)
							{
								if (t.Status == TaskStatus.Created)
									t.Start();

								if (
									t.Status != TaskStatus.Canceled &&
									t.Status != TaskStatus.Faulted &&
									t.Status != TaskStatus.RanToCompletion)
								{
								}
								t.Wait(this.Cancellation.Token);
							}
						}
						else
						{
							break;
						}
						//await Task.Delay(1);
					}
				}

				var tasks =
					Enumerable.Range(0, concurrent)
					.Select(i => Task.Run(() => worker()));

				Task.WhenAll(tasks).Wait();

				this.Cancellation.Dispose();
				this.Cancellation = null;
			});
		}

		public async Task Stop()
		{
			if (this.Cancellation == null)
				throw new InvalidOperationException("this TaskQueue is not started yet");

			this.Cancellation.Cancel();

			// cancelを待機
			while (this.Cancellation != null)
				await Task.Delay(1);
		}
	}
}

using System;
using Hangfire;

namespace Feedz.Worker.Jobs
{
	public class PingJob
	{
		public static void Run()
		{
			BackgroundJob.Enqueue(() => Console.WriteLine("Ping!"));
		}
	}
}


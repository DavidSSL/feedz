using System;
using Hangfire;

namespace Feedz.Worker.Jobs
{
	public class PingJob
	{
		public static void Schedule()
		{
			BackgroundJob.Enqueue(() => Run());
		}

		public static void Run()
		{
			Console.WriteLine("Ping!");
		}
	}
}


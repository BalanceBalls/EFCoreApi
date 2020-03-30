using Hangfire;

namespace EFCoreApi.Helpers
{
	/// <summary>
	/// Планировщик.
	/// </summary>
	public class JobScheduler
	{
		/// <summary>
		/// Создает задачу для планировщика.
		/// </summary>
		public static void ScheduleRecurringJobs()
		{
			RecurringJob.RemoveIfExists(nameof(UpdateDataJob));
			RecurringJob.AddOrUpdate<UpdateDataJob>(nameof(UpdateDataJob),
				job => job.Run(JobCancellationToken.Null), Cron.Minutely);
		}
	}
}
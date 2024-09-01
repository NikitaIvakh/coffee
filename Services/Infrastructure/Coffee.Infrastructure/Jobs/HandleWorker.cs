using Hangfire;

namespace Coffee.Infrastructure.Jobs;

public static class HandleWorker
{
    public static void StartRecurringJobs()
    {
        RecurringJob.AddOrUpdate<IImageCleanUpJob>("image-cleaner", job => job.ProcessAsync(), Cron.Daily);
    }
}
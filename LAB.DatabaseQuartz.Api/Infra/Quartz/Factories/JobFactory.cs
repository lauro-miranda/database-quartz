using Quartz.Spi;
using Quartz;

namespace LAB.DatabaseQuartz.Api.Infra.Quartz.Factories
{
    public class JobFactory : IJobFactory
    {
        IServiceProvider ServiceProvider { get; }

        public JobFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
            => ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;

        public void ReturnJob(IJob job) => (job as IDisposable)?.Dispose();
    }
}
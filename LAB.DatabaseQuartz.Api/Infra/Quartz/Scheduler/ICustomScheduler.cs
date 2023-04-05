using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Spi;

namespace LAB.DatabaseQuartz.Api.Infra.Quartz.Scheduler
{
    public interface ICustomScheduler
    {
        IScheduler Value { get; }

        Task ExecuteAsync(string jobName, string groupName);
    }

    public class CustomScheduler : ICustomScheduler
    {
        public IScheduler Value { get; }

        public CustomScheduler(ISchedulerFactory schedulerFactory
            , IJobFactory factory)
        {
            Value = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
            Value.JobFactory = factory;
        }

        public async Task ExecuteAsync(string jobName, string groupName)
        {
            var matcher = GroupMatcher<JobKey>.GroupEquals(groupName);
            var executionJobs = await Value.GetJobKeys(matcher);

            var key = executionJobs.Where(j => j.Name == jobName).SingleOrDefault();

            if (key == null) return;

            await Value.TriggerJob(key);
        }
    }
}
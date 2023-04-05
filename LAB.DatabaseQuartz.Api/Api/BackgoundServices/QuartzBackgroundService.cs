using LAB.DatabaseQuartz.Api.Infra.Quartz.Jobs;
using LAB.DatabaseQuartz.Api.Infra.Quartz.Models;
using LAB.DatabaseQuartz.Api.Infra.Quartz.Repositories.Contracts;
using LAB.DatabaseQuartz.Api.Infra.Quartz.Scheduler;
using Quartz;
using Quartz.Impl.Matchers;

namespace LAB.DatabaseQuartz.Api.Api.BackgoundServices
{
    public class QuartzBackgroundService : BackgroundService
    {
        ICustomScheduler Scheduler { get; }

        IJobRepository JobRepository { get; }

        public QuartzBackgroundService(ICustomScheduler customScheduler
            , IJobRepository jobRepository)
        {
            Scheduler = customScheduler;

            JobRepository = jobRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Scheduler.Value.Start(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                var jobs = await JobRepository.GetJobAsync();

                foreach (var job in jobs)
                {
                    if (await CheckExists(job, stoppingToken)) continue;

                    IJobDetail detail = CreateDetail(job);

                    TriggerBuilder triggerBuilder = CreateTrigger(job);
                    
                    CreateCron(job, triggerBuilder);

                    await Scheduler.Value.ScheduleJob(detail, triggerBuilder.Build(), stoppingToken);
                }

                await Task.Delay(5000);
            }
        }

        static IJobDetail CreateDetail(JobModel job)
        {
            var type = Type.GetType($"{typeof(ConciliationJob).Namespace}.{job.Name}");

            if (type == null)
                throw new NullReferenceException(nameof(type));

            return JobBuilder
                .Create(type)
                .WithIdentity(job.Name, job.Group.Name)
                .UsingJobData("configId", job.Code)
                .Build();
        }

        static TriggerBuilder CreateTrigger(JobModel job)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity("DefaultTrigger", job.Group.Name);
        }

        static void CreateCron(JobModel job, TriggerBuilder triggerBuilder)
        {
            CronExpression cron = new(job.CronExpression);

            if (cron != null)
            {
                var a = cron.GetNextValidTimeAfter(DateTimeOffset.Now);

                if (a != null)
                {
                    triggerBuilder.WithCronSchedule(job.CronExpression).StartAt(a.Value);
                }
            }
        }

        async Task<bool> CheckExists(JobModel job, CancellationToken stoppingToken)
        {
            var matcher = GroupMatcher<JobKey>.GroupEquals(job.Group.Name);
            var executionJobs = await Scheduler.Value.GetJobKeys(matcher, stoppingToken);
            var currentKey = executionJobs.Where(j => j.Name == job.Name).SingleOrDefault();
            
            return currentKey != null && await Scheduler.Value.CheckExists(currentKey, stoppingToken);
        }
    }
}
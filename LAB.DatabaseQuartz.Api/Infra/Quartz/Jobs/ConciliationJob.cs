using LAB.DatabaseQuartz.Api.Domain.Services.Contracts;
using Newtonsoft.Json;
using Quartz;

namespace LAB.DatabaseQuartz.Api.Infra.Quartz.Jobs
{
    public class ConciliationJob : IJob
    {
        IDomainService DomainService { get; }

        public ConciliationJob(IDomainService domainService)
        {
            DomainService = domainService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            DomainService.Print(JsonConvert.SerializeObject(context.JobDetail));
            return Task.CompletedTask;
        }
    }
}
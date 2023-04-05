using LAB.DatabaseQuartz.Api.Domain.Services.Contracts;
using LAB.DatabaseQuartz.Api.Infra.Quartz.Jobs.Models;
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
            var data = context.JobDetail.JobDataMap.GetString("Data");

            if (string.IsNullOrEmpty(data)) 
            {
                return Task.CompletedTask;
            }

            var model = JsonConvert.DeserializeObject<ConciliationModel>(data);

            Console.WriteLine(data);

            return Task.CompletedTask;
        }
    }
}
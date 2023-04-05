using LAB.DatabaseQuartz.Api.Domain.Services.Contracts;
using LAB.DatabaseQuartz.Api.Infra.Quartz.Jobs.Models;
using LAB.DatabaseQuartz.Api.Infra.Quartz.Models;
using Newtonsoft.Json;
using Quartz;

namespace LAB.DatabaseQuartz.Api.Infra.Quartz.Jobs
{
    public class EmailJob : IJob
    {
        IDomainService DomainService { get; }

        public EmailJob(IDomainService domainService)
        {
            DomainService = domainService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var data = context.JobDetail.JobDataMap.GetString(nameof(JobModel.Data));

            if (string.IsNullOrEmpty(data)) 
            {
                return Task.CompletedTask;
            }

            var model = JsonConvert.DeserializeObject<EmailModel>(data);

            DomainService.Proccess(data);

            return Task.CompletedTask;
        }
    }
}
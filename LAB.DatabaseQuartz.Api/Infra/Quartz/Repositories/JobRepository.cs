using LAB.DatabaseQuartz.Api.Infra.Quartz.Jobs;
using LAB.DatabaseQuartz.Api.Infra.Quartz.Models;
using LAB.DatabaseQuartz.Api.Infra.Quartz.Repositories.Contracts;

namespace LAB.DatabaseQuartz.Api.Infra.Quartz.Repositories
{
    public class JobRepository : IJobRepository
    {
        ICollection<JobModel> Jobs { get; }

        public JobRepository()
        {
            Jobs = new List<JobModel>
            {
                new JobModel(Guid.Parse("665B9037-566D-49C6-B6DE-887DFC783017"), nameof(ConciliationJob)) { Group = new JobModel.GroupModel(Guid.Parse("665B9037-566D-49C6-B6DE-887DFC783017")) },
                new JobModel(Guid.Parse("53D5717D-1224-464B-9421-B474B8F6341B"), nameof(EmailJob)) { Group = new JobModel.GroupModel(Guid.Parse("53D5717D-1224-464B-9421-B474B8F6341B")) },
            };
        }

        public async Task<List<JobModel>> GetJobAsync()
        {
            return await Task.FromResult(Jobs.ToList());
        }

        public async Task<JobModel> GetJobAsync(Guid code)
        {
            return await Task.FromResult(Jobs.First(j => j.Code == code));
        }
    }
}
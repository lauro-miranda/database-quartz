using LAB.DatabaseQuartz.Api.Infra.Quartz.Models;

namespace LAB.DatabaseQuartz.Api.Infra.Quartz.Repositories.Contracts
{
    public interface IJobRepository
    {
        Task<List<JobModel>> GetJobAsync();

        Task<JobModel> GetJobAsync(Guid code);
    }
}
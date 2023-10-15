using Speedruns.Backend.Models;

namespace Speedruns.Backend.Interfaces
{
    public interface IRunRepository
    {
        Task<List<RunModel>> GetAll();
        Task<RunModel> GetById(long id);
        Task<List<RunModel>> GetUserRuns(long id);
        Task<RunModel> CreateRun(RunModel run);
        Task<RunModel> UpdateRun(RunModel runToUpdate, RunModel run);
        Task DeleteRun(RunModel run);
    }
}

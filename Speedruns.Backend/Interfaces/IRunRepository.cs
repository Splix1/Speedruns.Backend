using Speedruns.Backend.Models;

namespace Speedruns.Backend.Interfaces
{
    public interface IRunRepository
    {
        Task<List<RunModel>> GetAll();
        Task<RunModel> GetById(long id);
        Task<RunModel> CreateRun(RunModel run);
        Task<RunModel> UpdateRun(long id, RunModel run);
        Task<RunModel> DeleteRun(long id);
    }
}

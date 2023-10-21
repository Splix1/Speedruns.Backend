using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Interfaces
{
    public interface IRunRepository
    {
        Task<List<RunEntity>> GetAll();
        Task<RunEntity> GetById(long id);
        Task<List<RunEntity>> GetUserRuns(long? id);
        Task<RunEntity> CreateRun(RunEntity run);
        Task<RunEntity> UpdateRun(RunEntity runToUpdate, RunEntity run);
        Task DeleteRun(RunEntity run);
    }
}

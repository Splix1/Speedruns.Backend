using Speedruns.Backend.Models;

namespace Speedruns.Backend.Interfaces
{
    public interface ICommentsRepository
    {
        Task<List<CommentModel>> GetComments(long runId);
    }
}

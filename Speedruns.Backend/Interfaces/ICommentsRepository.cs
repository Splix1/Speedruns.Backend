using Speedruns.Backend.Models;

namespace Speedruns.Backend.Interfaces
{
    public interface ICommentsRepository
    {
        Task<List<CommentModel>> GetComments(long runId);
        Task<CommentModel> AddComment(CommentModel comment);
        Task UpdateComment (CommentModel comment);
        Task DeleteComment(CommentModel comment);
    }
}

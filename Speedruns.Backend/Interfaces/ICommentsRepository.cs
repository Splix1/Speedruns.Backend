using Speedruns.Backend.Models;

namespace Speedruns.Backend.Interfaces
{
    public interface ICommentsRepository
    {
        Task<List<CommentEntity>> GetComments(long runId);
        Task<CommentEntity> GetCommentById(long id);
        Task<CommentEntity> AddComment(CommentEntity comment);
        Task UpdateComment (CommentEntity comment);
        Task DeleteComment(CommentEntity comment);
    }
}

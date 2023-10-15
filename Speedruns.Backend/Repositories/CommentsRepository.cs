using Microsoft.EntityFrameworkCore;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Models;

namespace Speedruns.Backend.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly SpeedrunsContext _context;
        private readonly DbSet<CommentModel> _comments;

        public CommentsRepository(SpeedrunsContext context)
        {
            _context = context;
            _comments = context.Comments;
        }
        public async Task<List<CommentModel>> GetComments(long runId)
        {
            return await _comments.Where(comment => comment.RunId == runId).ToListAsync();
        }
    }
}

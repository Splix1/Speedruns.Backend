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
            return await _comments.Include(x => x.Run).Include(x => x.User).Where(comment => comment.RunId == runId).ToListAsync();
        }

        public async Task<CommentModel> GetCommentById(long id)
        {
            return await _comments.Include(x => x.User).Include(x => x.Run).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CommentModel> AddComment(CommentModel comment)
        {
            _comments.Add(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task UpdateComment(CommentModel comment)
        {
            _comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComment(CommentModel comment)
        {
            _comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}

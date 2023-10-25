using Microsoft.EntityFrameworkCore;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly SpeedrunsContext _context;
        private readonly DbSet<CommentEntity> _comments;

        public CommentsRepository(SpeedrunsContext context)
        {
            _context = context;
            _comments = context.Set<CommentEntity>();
        }
        public async Task<List<CommentEntity>> GetComments(long runId)
        {
            return await _comments.Include(x => x.Run).Include(x => x.User).Where(comment => comment.RunId == runId).ToListAsync();
        }

        public async Task<CommentEntity> GetCommentById(long id)
        {
            return await _comments.Include(x => x.User).Include(x => x.Run).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CommentEntity> AddComment(CommentEntity comment)
        {
            _comments.Add(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task UpdateComment(CommentEntity comment)
        {
            _comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComment(CommentEntity comment)
        {
            _comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}

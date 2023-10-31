using Speedruns.Backend.Entities;
using Speedruns.Backend.Repositories;
using Speedruns.Backend.Tests.Database;

namespace Speedruns.Backend.Tests._Fixtures.Repositories
{
    public class CommentsRepositoryFixture
    {
        private DbContextMock<CommentEntity> _dbContextMock;
        private CommentsRepository _commentsRepository;

        public CommentsRepositoryFixture()
        {
            _dbContextMock = new DbContextMock<CommentEntity>(new List<CommentEntity>
            {
                new CommentEntity { Id = 1, Date = DateTime.UtcNow, Text = "Comment 1", UserId = 1, RunId = 1 },
                new CommentEntity { Id = 2, Date = DateTime.UtcNow, Text = "Comment 2", UserId = 1, RunId = 1 },
            });

            _commentsRepository = new CommentsRepository(_dbContextMock.Context);
        }

        internal CommentsRepository Repository => _commentsRepository;

        public void ResetRepository()
        {
            _dbContextMock = new DbContextMock<CommentEntity>(new List<CommentEntity>
            {
                new CommentEntity { Id = 1, Date = DateTime.UtcNow, Text = "Comment 1", UserId = 1, RunId = 1 },
                new CommentEntity { Id = 2, Date = DateTime.UtcNow, Text = "Comment 2", UserId = 1, RunId = 1 },
            });

            _commentsRepository = new CommentsRepository(_dbContextMock.Context);
        }
       
    }
}

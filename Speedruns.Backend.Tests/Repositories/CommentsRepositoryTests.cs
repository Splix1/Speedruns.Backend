using Speedruns.Backend.Entities;
using Speedruns.Backend.Repositories;
using Speedruns.Backend.Tests.Database;

namespace Speedruns.Backend.Tests.Repositories
{
    public class CommentsRepositoryTests
    {
        private DbContextMock<CommentEntity> _dbContextMock;
        private CommentsRepository _commentsRepository;

        public CommentsRepositoryTests()
        {
            _dbContextMock = new DbContextMock<CommentEntity>(new List<CommentEntity>
            {
                new CommentEntity { Id = 1, Date = DateTime.UtcNow, Text = "Comment 1", UserId = 1, RunId = 1 },
                new CommentEntity { Id = 2, Date = DateTime.UtcNow, Text = "Comment 2", UserId = 1, RunId = 1 },
            });

            _commentsRepository = new CommentsRepository(_dbContextMock.Context);
        }

        [Fact]
        public async Task ShouldReturnCommentsByRunId()
        {
            var comments = await _commentsRepository.GetComments(1);

            Assert.NotNull(comments);
            Assert.IsType<List<CommentEntity>>(comments);
            Assert.Equal(2, comments.Count);
            Assert.Equal("Comment 1", comments.First().Text);
            Assert.Equal("Comment 2", comments.Last().Text);
        }

        [Fact]
        public async Task ShouldReturnCommentByCommentId()
        {
            var comment = await _commentsRepository.GetCommentById(1);

            Assert.NotNull(comment);
            Assert.IsType<CommentEntity>(comment);
            Assert.Equal(1, comment.Id);
            Assert.Equal("Comment 1", comment.Text);
        }

      
    }
}

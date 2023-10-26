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

        [Fact]
        public async Task ShouldReturnNullCommentByCommentId()
        {
            var comment = await _commentsRepository.GetCommentById(100);

            Assert.Null(comment);
        }

        [Fact]
        public async Task ShouldAddComment()
        {
            var newComment = new CommentEntity { Id = 3, Date = DateTime.UtcNow, Text = "Comment 3", UserId = 1, RunId = 1 };

            await _commentsRepository.AddComment(newComment);

            var comment = await _commentsRepository.GetCommentById(3);

            Assert.NotNull(comment);
            Assert.IsType<CommentEntity>(comment);
            Assert.Equal(3, comment.Id);
            Assert.Equal("Comment 3", comment.Text);
        }

        [Fact]
        public async Task ShouldUpdateComment()
        {
            var newComment = new CommentEntity { Id = 1, Date = DateTime.UtcNow, Text = "Comment 1 Updated", UserId = 1, RunId = 1 };

            await _commentsRepository.UpdateComment(newComment);

            var comment = await _commentsRepository.GetCommentById(1);

            Assert.NotNull(comment);
            Assert.IsType<CommentEntity>(comment);
            Assert.Equal(1, comment.Id);
            Assert.Equal("Comment 1 Updated", comment.Text);
        }

        [Fact]
        public async Task ShouldDeleteComment()
        {
            var commentToDelete = await _commentsRepository.GetCommentById(1);

            await _commentsRepository.DeleteComment(commentToDelete);

            var comment = await _commentsRepository.GetCommentById(1);

            Assert.Null(comment);
        }
    }
}

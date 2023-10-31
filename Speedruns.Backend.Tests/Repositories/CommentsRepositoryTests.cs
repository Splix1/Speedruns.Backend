using Speedruns.Backend.Entities;
using Speedruns.Backend.Tests._Fixtures.Repositories;

namespace Speedruns.Backend.Tests.Repositories
{
    public class CommentsRepositoryTests : IClassFixture<CommentsRepositoryFixture>
    {
        private readonly CommentsRepositoryFixture _fixture;

        public CommentsRepositoryTests(CommentsRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldReturnCommentsByRunId()
        {
            _fixture.ResetRepository();

            var comments = await _fixture.Repository.GetComments(1);

            Assert.NotNull(comments);
            Assert.IsType<List<CommentEntity>>(comments);
            Assert.Equal(2, comments.Count);
            Assert.Equal("Comment 1", comments.First().Text);
            Assert.Equal("Comment 2", comments.Last().Text);
        }

        [Fact]
        public async Task ShouldReturnCommentByCommentId()
        {
            _fixture.ResetRepository();
            var comment = await _fixture.Repository.GetCommentById(1);

            Assert.NotNull(comment);
            Assert.IsType<CommentEntity>(comment);
            Assert.Equal(1, comment.Id);
            Assert.Equal("Comment 1", comment.Text);
        }

        [Fact]
        public async Task ShouldReturnNullCommentByCommentId()
        {
            _fixture.ResetRepository();
            var comment = await _fixture.Repository.GetCommentById(100);

            Assert.Null(comment);
        }

        [Fact]
        public async Task ShouldAddComment()
        {
            _fixture.ResetRepository();
            var newComment = new CommentEntity { Id = 3, Date = DateTime.UtcNow, Text = "Comment 3", UserId = 1, RunId = 1 };

            await _fixture.Repository.AddComment(newComment);

            var comment = await _fixture.Repository.GetCommentById(3);

            Assert.NotNull(comment);
            Assert.IsType<CommentEntity>(comment);
            Assert.Equal(3, comment.Id);
            Assert.Equal("Comment 3", comment.Text);
        }

        [Fact]
        public async Task ShouldUpdateComment()
        {
            _fixture.ResetRepository();
            var newComment = new CommentEntity { Id = 1, Date = DateTime.UtcNow, Text = "Comment 1 Updated", UserId = 1, RunId = 1 };

            await _fixture.Repository.UpdateComment(newComment);

            var comment = await _fixture.Repository.GetCommentById(1);

            Assert.NotNull(comment);
            Assert.IsType<CommentEntity>(comment);
            Assert.Equal(1, comment.Id);
            Assert.Equal("Comment 1 Updated", comment.Text);
        }

        [Fact]
        public async Task ShouldDeleteComment()
        {
            _fixture.ResetRepository();
            var commentToDelete = await _fixture.Repository.GetCommentById(1);

            await _fixture.Repository.DeleteComment(commentToDelete);

            var comment = await _fixture.Repository.GetCommentById(1);

            Assert.Null(comment);
        }
    }
}

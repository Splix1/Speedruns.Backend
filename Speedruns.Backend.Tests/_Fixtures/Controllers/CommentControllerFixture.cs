using NSubstitute;
using Speedruns.Backend.Interfaces;

namespace Speedruns.Backend.Tests._Fixtures.Controllers
{
    public class CommentControllerFixture
    {
        private ICommentsRepository _commentsRepository;
        private IRunRepository _runRepository;

        public CommentControllerFixture()
        {
            _commentsRepository = Substitute.For<ICommentsRepository>();
            _runRepository = Substitute.For<IRunRepository>();
        }

        internal ICommentsRepository CommentsRepository => _commentsRepository;
        internal IRunRepository RunRepository => _runRepository;

        public void ResetRepositories()
        {
            _commentsRepository = Substitute.For<ICommentsRepository>();
            _runRepository = Substitute.For<IRunRepository>();
        }
    }
}

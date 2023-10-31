using Speedruns.Backend.Entities;
using Speedruns.Backend.Tests._Fixtures.Repositories;

namespace Speedruns.Backend.Tests.Repositories
{
    public class UserRepositoryTests : IClassFixture<UserRepositoryFixture>
    {

        private readonly UserRepositoryFixture _fixture;

        public UserRepositoryTests(UserRepositoryFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public async Task ShouldReturnListOfUsers()
        {
            _fixture.ResetRepository();
            var users = await _fixture.Repository.GetAll();

            Assert.NotNull(users);
            Assert.IsType<List<UserEntity>>(users);
            Assert.True(users.First().Runs.Any());
            Assert.Equal(2, users.Count);
        }

        [Fact]
        public async Task ShouldReturnUserById()
        {
            _fixture.ResetRepository();
            var user = await _fixture.Repository.GetById(1);

            Assert.NotNull(user);
            Assert.IsType<UserEntity>(user);
            Assert.True(user.Runs.Any());
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public async Task ShouldReturnNullUserById()
        {
            _fixture.ResetRepository();
            var user = await _fixture.Repository.GetById(100);

            Assert.Null(user);
        }

        [Fact]
        public async Task ShouldReturnUserByName()
        {
            _fixture.ResetRepository();
            var user = await _fixture.Repository.GetByName("Test 1");

            Assert.NotNull(user);
            Assert.IsType<UserEntity>(user);
            Assert.Equal("Test 1", user.UserName);
            Assert.True(user.Runs.Any());
        }

        [Fact]
        public async Task ShouldReturnNullUserByName()
        {
            _fixture.ResetRepository();
            var user = await _fixture.Repository.GetByName("Test 3");

            Assert.Null(user);
        }

        [Fact]
        public async Task ShouldReturnCreatedUser()
        {
            _fixture.ResetRepository();
            var user = new UserEntity { Id = 3, UserName = "Test 3" };

            var createdUser = await _fixture.Repository.CreateUser(user);

            Assert.NotNull(createdUser);
            Assert.IsType<UserEntity>(createdUser);
            Assert.Equal(3, createdUser.Id);
            Assert.Equal("Test 3", createdUser.UserName);
        }

        [Fact]
        public async Task ShouldReturnUpdatedUser()
        {
            _fixture.ResetRepository();
            var user = new UserEntity { Id = 1, UserName = "Testing 1" };

            var updatedUser = await _fixture.Repository.UpdateUser(user.Id, user);

            Assert.NotNull(updatedUser);
            Assert.IsType<UserEntity>(updatedUser);
            Assert.Equal(1, updatedUser.Id);
            Assert.Equal("Testing 1", updatedUser.UserName);
        }

        [Fact]
        public async Task ShouldReturnNullUpdatedUser()
        {
            _fixture.ResetRepository();
            var user = new UserEntity { Id = 100, UserName = "Testing 100" };

            var updatedUser = await _fixture.Repository.UpdateUser(user.Id, user);

            Assert.Null(updatedUser);
        }

        [Fact]
        public async Task ShouldDeleteUser()
        {
            _fixture.ResetRepository();
            var userToDelete = await _fixture.Repository.GetById(1);

            await _fixture.Repository.DeleteUser(userToDelete);

            var deletedUser = await _fixture.Repository.GetById(1);

            Assert.Null(deletedUser);
        }
    }
}

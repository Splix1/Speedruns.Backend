using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetAll();
        Task<UserEntity> GetById(long id);
        Task<UserEntity> GetByName(string name);
        Task<UserEntity> CreateUser(UserEntity user);
        Task UpdateUser(long id, UserEntity user);

        Task DeleteUser(UserEntity user);
    }
}

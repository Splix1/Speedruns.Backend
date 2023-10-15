using Speedruns.Backend.Models;

namespace Speedruns.Backend.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAll();
        Task<UserModel> GetById(long id);
        Task<UserModel> GetByName(string name);
        Task<UserModel> CreateUser(UserModel user);
        Task UpdateUser(long id, UserModel user);

        Task DeleteUser(UserModel user);
    }
}

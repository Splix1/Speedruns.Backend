using Microsoft.EntityFrameworkCore;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Models;

namespace Speedruns.Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SpeedrunsContext _context;
        private readonly DbSet<UserModel> _users;

        public UserRepository(SpeedrunsContext context)
        {
            _context = context;
            _users = context.Users;
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await _users.Include(x => x.Runs).ToListAsync();
        }

        public async Task<UserModel> GetById(long id)
        {
            return await _users.Include(x => x.Runs).FirstAsync(x => x.Id == id);
        }

        public async Task<UserModel> GetByName(string username)
        {
            return await _users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<UserModel> CreateUser(UserModel user)
        {
           
            _users.Add(user);
            await _context.SaveChangesAsync();
            return await GetByName(user.UserName);
        }

        public async Task UpdateUser(long id, UserModel user)
        {

            var userToUpdate = await _users.FindAsync(id);

            userToUpdate.UserName = user.UserName;
            userToUpdate.ImageUrl = user.ImageUrl;
            userToUpdate.YoutubeLink = user.YoutubeLink;
            userToUpdate.TwitchLink = user.TwitchLink;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(UserModel user)
        {

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}

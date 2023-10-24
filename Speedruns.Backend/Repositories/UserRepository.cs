using Microsoft.EntityFrameworkCore;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SpeedrunsContext _context;
        private readonly DbSet<UserEntity> _users;
        private readonly DbSet<RunEntity> _runs;

        public UserRepository(SpeedrunsContext context)
        {
            _context = context;
            _users = context.Set<UserEntity>();
            _runs = context.Set<RunEntity>();
        }

        public async Task<List<UserEntity>> GetAll()
        {
            return await _users.Include(x => x.Runs).ToListAsync();
        }

        public async Task<UserEntity> GetById(long id)
        {
            return await _users.Include(x => x.Runs).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserEntity> GetByName(string username)
        {
            return await _users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<UserEntity> CreateUser(UserEntity user)
        {
           
            _users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserEntity> UpdateUser(long id, UserEntity user)
        {

            var userToUpdate = await _users.FindAsync(id);

            if (userToUpdate == null)
            {
                return null;
            }

            userToUpdate.UserName = user.UserName;
            userToUpdate.ImageUrl = user.ImageUrl;
            userToUpdate.YoutubeLink = user.YoutubeLink;
            userToUpdate.TwitchLink = user.TwitchLink;

            await _context.SaveChangesAsync();

            return userToUpdate;
        }

        public async Task DeleteUser(UserEntity user)
        {
            // pull and update UserName column on runs upon user deletion
            var runs = _runs.AsQueryable().Where(x => x.UserId == user.Id);

            foreach(var run in runs)
            {
                run.UserName = user.UserName;
            }
           
            _users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}

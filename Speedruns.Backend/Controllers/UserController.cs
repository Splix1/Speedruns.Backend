using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Speedruns.Backend.Models;


namespace Speedruns.Backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbSet<UserModel> _users;
        private readonly SpeedrunsContext _context;

        public UserController(SpeedrunsContext context)
        {
            _users = context.Users;
            _context = context;
        }

        // GET: /api/users
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAll()
        {
            try
            {
                var users = await _users.ToListAsync();
                return Ok(users);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // GET: /api/users/id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUser(long id)
        {
            try
            {
                return Ok(await _users.FindAsync(id));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // POST: /api/users
        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel user)
        {
            try
            {
                if (_users == null)
                {
                    return Problem("Model set 'SpeedrunsContext.Users' is null.");
                }

                if(_users.Contains(user))
                {
                    return BadRequest("User already exists.");
                }

                _users.Add(user);
                await _context.SaveChangesAsync();
                
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // PUT: /api/users/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> UpdateUser(long id, UserModel user)
        {
            try
            {
                if (_users == null)
                {
                    return Problem("Model set 'SpeedrunsContext.Users' is null.");
                }

                var userToUpdate = await _users.FindAsync(id);
                if (userToUpdate == null)
                {
                    return NotFound("User does not exist.");
                }

                userToUpdate.UserName = user.UserName;
                userToUpdate.ImageUrl = user.ImageUrl;
                userToUpdate.YoutubeLink = user.YoutubeLink;
                userToUpdate.TwitchLink = user.TwitchLink;
                
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUser", new {id = user.Id}, user);
            }
             catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}

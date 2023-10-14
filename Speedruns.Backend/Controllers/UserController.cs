using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Speedruns.Backend.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Speedruns.Backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbSet<UserModel> _users;

        public UserController(SpeedrunsContext context)
        {
            _users = context.Users;
        }

        // GET: /api/users
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAll()
        {
            try
            {
                var users = _users.ToListAsync();
                return Ok(users);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}

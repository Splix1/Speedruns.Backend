using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Speedruns.Backend.Entities;
using Speedruns.Backend.Interfaces;

namespace Speedruns.Backend.Controllers
{
    [Route("api/games)")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGamesRepository _games;
        public GameController(IGamesRepository games)
        {
            _games = games;
        }

        // GET: /api/games
        [HttpGet]
        public async Task<ActionResult<List<GameEntity>>> GetAll()
        {
            try
            {

                var games = await _games.GetAll();

                return Ok(games);
            } catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // GET: /api/games/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GameEntity>> GetById(long id)
        {
            try
            {
                var game = await _games.GetById(id);

                if(game == null)
                {
                    return NotFound("Game not found.");
                }

                return Ok(game);
            }
             catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // GET: /api/games/{name}
        [HttpGet("{name}")]
        public async Task<ActionResult<GameEntity>> GetByName(string name)
        {
            try
            {
                var game = await (_games.GetByName(name));
                
                if(game == null)
                {
                    return NotFound("Game not found.");
                }

                return Ok(game);
            } catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}

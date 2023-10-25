using Microsoft.AspNetCore.Mvc;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Entities;

namespace Speedruns.Backend.Controllers
{
    [Route("api/runs")]
    [ApiController]
    public class RunController : ControllerBase
    {
        private readonly IRunRepository _runs;
        private readonly IGamesRepository _games;

        public RunController(IRunRepository runs, IGamesRepository games)
        {
            _runs = runs;
            _games = games;
        }

        // GET: /api/runs
        [HttpGet]
        public async Task<ActionResult<List<RunEntity>>> GetAll()
        {
            try
            {
                return Ok(await _runs.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // GET: /api/runs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RunEntity>> GetById(long id)
        {
            try
            {
                return Ok(await _runs.GetById(id));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // POST: /api/runs
        [HttpPost]
        public async Task<ActionResult<RunEntity>> CreateRun(RunEntity run)
        {
            try
            {
                var runs = await _runs.GetUserRuns(run.UserId);

                if (runs.Contains(run))
                {
                    return BadRequest("Run already exists.");
                }

                var game = await _games.GetById(run.GameId);

                if (game == null)
                {
                    return NotFound("Game not found.");
                }

                return Ok(await _runs.CreateRun(run, game));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // PUT: /api/runs/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<RunEntity>> UpdateRun(long id, RunEntity run)
        {
            try
            {
                var runToUpdate = await _runs.GetById(id);

                if (runToUpdate == null)
                {
                    return NotFound();
                }

                return Ok(await _runs.UpdateRun(runToUpdate, run));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);

            }
        }

        // DELETE: /api/runs/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRun(long id)
        {
            try
            {
                var run = await _runs.GetById(id);

                if (run == null )
                {
                    return NotFound("Run does not exist.");
                }

                await _runs.DeleteRun(run);
                return Ok("Run successfully deleted.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}

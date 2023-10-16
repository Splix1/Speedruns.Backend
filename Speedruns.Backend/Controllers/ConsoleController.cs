using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Speedruns.Backend.Models;
using Speedruns.Backend.Interfaces;

namespace Speedruns.Backend.Controllers
{
    [Route("api/consoles")]
    [ApiController]
    public class ConsoleController : ControllerBase
    {
        private readonly IConsolesRepository _consoles;
        public ConsoleController(IConsolesRepository consoles)
        {
            _consoles = consoles;
        }

        // GET: /api/consoles
        [HttpGet]
        public async Task<ActionResult<List<ConsoleModel>>> GetAll()
        {
            try
            {
                if (_consoles == null)
                {
                    return NotFound("Consoles is empty.");
                }

                return Ok(await _consoles.GetAll());

            } catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}

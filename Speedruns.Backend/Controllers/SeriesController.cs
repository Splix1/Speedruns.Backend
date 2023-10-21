using Microsoft.AspNetCore.Mvc;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Models;

namespace Speedruns.Backend.Controllers
{
    [Route("api/series")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesRepository _series;

        public SeriesController(ISeriesRepository series)
        {
            _series = series;
        }

        public async Task<ActionResult<List<SeriesEntity>>> GetAll()
        {
            try
            {
                if(_series == null)
                {
                    return BadRequest("Series is empty.");
                }

                return Ok(await _series.GetAll());
            } catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        public async Task<ActionResult<SeriesEntity>> GetById(long id)
        {
            try
            {
                var game = await _series.GetById(id);
                if(game == null)
                {
                    return NotFound("Game does not exist.");
                }

                return Ok(game);

            } catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }


        public async Task<ActionResult<SeriesEntity>> GetByName(string name)
        {
            try
            {
                var game = await _series.GetByName(name);

                if( game == null)
                {
                    return NotFound("Game does not exist.");
                }

                return Ok(game);
            }catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}

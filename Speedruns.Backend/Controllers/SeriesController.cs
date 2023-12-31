﻿using Microsoft.AspNetCore.Mvc;
using Speedruns.Backend.Interfaces;
using Speedruns.Backend.Entities;

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

        [HttpGet]
        public async Task<ActionResult<List<SeriesEntity>>> GetAll()
        {
            try
            {
               var series = await _series.GetAll();

                return Ok(series);
            } catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
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


        [HttpGet("{name}")]
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

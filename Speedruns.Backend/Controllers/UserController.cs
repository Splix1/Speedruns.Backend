﻿using Microsoft.AspNetCore.Mvc;
using Speedruns.Backend.Models;
using Speedruns.Backend.Interfaces;

namespace Speedruns.Backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _users;
        

        public UserController(IUserRepository users)
        {
            _users = users;
           
        }

        // GET: /api/users
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAll()
        {
            try
            { 
                var users = await _users.GetAll();
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
        public async Task<ActionResult<UserModel>> GetById(long id)
        {
            try
            {
                return Ok(await _users.GetById(id));
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

                var ifUserExists = await _users.GetByName(user.UserName);
                if (ifUserExists != null)
                {
                    return BadRequest("User with that name already exists.");
                }

                await _users.CreateUser(user);
                
                return Ok(await _users.GetByName(user.UserName));
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

                var userToUpdate = await _users.GetById(id);
                if (userToUpdate == null)
                {
                    return NotFound("User does not exist.");
                }

                await _users.UpdateUser(id, user);

                return Ok(await _users.GetById(id));
            }
             catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // DELETE: /api/users/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(long id)
        {
            try
            {
                var user = await _users.GetById(id);
                if (user == null)
                { 
                    return NotFound(); 
                }

                await _users.DeleteUser(user);

                return Ok("User successfully deleted.");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}
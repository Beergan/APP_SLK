using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuomRaCoffe.API.Data;
using RuomRaCoffe.API.Data.Entities;
using RuomRaCoffe.Shared;

namespace RuomRaCoffe.Admin.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class UserController : ControllerBase
    {

        private readonly DataContext _context;
        
        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                    return BadRequest(new { error = "User data is required" });

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }
        
        
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RuomRaCoffe.API.Data;
using RuomRaCoffe.API.Data.Entities;
using RuomRaCoffe.Shared.Dtos;

namespace RuomRaCoffe.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        
        public UserController(DataContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
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

        [HttpGet("staff")]
        public async Task<IActionResult> GetStaff()
        {
            try
            {
                var staff = await _context.Users
                    .Where(u => u.Role == "Staff" || u.Role == "Manager" || u.Role == "Admin")
                    .ToListAsync();
                return Ok(staff);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return NotFound(new { error = "User not found" });

                return Ok(user);
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

                // Hash password
                user.Password = _passwordHasher.HashPassword(user, user.Password);
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        [HttpPost("staff")]
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaffDto createStaffDto)
        {
            try
            {
                if (createStaffDto == null)
                    return BadRequest(new { error = "Staff data is required" });

                // Validate email uniqueness
                if (await _context.Users.AnyAsync(u => u.Email == createStaffDto.Email))
                {
                    return BadRequest(new { error = "Email already exists" });
                }

                // Create new user
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = createStaffDto.Name,
                    Email = createStaffDto.Email,
                    Phone = createStaffDto.Phone,
                    Role = createStaffDto.Role,
                    Address = createStaffDto.Address,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                // Hash password
                newUser.Password = _passwordHasher.HashPassword(newUser, createStaffDto.Password);

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // Return user without password
                var responseUser = new
                {
                    newUser.Id,
                    newUser.Name,
                    newUser.Email,
                    newUser.Phone,
                    newUser.Role,
                    newUser.Address,
                    newUser.CreatedAt,
                    newUser.UpdatedAt
                };

                return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, responseUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateStaffDto updateStaffDto)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return NotFound(new { error = "User not found" });

                // Check email uniqueness if email is being changed
                if (updateStaffDto.Email != user.Email && 
                    await _context.Users.AnyAsync(u => u.Email == updateStaffDto.Email))
                {
                    return BadRequest(new { error = "Email already exists" });
                }

                // Update user properties
                user.Name = updateStaffDto.Name;
                user.Email = updateStaffDto.Email;
                user.Phone = updateStaffDto.Phone;
                user.Role = updateStaffDto.Role;
                user.Address = updateStaffDto.Address;
                user.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return NotFound(new { error = "User not found" });

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok(new { message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        [HttpGet("staff/statistics")]
        public async Task<IActionResult> GetStaffStatistics()
        {
            try
            {
                var totalStaff = await _context.Users
                    .CountAsync(u => u.Role == "Staff" || u.Role == "Manager" || u.Role == "Admin");

                var today = DateTime.Today;
                var todayShifts = await _context.ShiftRecords
                    .Where(s => s.CheckInTime.Date == today)
                    .ToListAsync();

                var currentlyWorking = todayShifts.Count(s => s.CheckOutTime == null);
                var averageWorkHours = todayShifts
                    .Where(s => s.CheckOutTime.HasValue)
                    .Select(s => (s.CheckOutTime.Value - s.CheckInTime).TotalHours)
                    .DefaultIfEmpty(0)
                    .Average();

                var statistics = new StaffStatisticsDto
                {
                    TotalStaff = totalStaff,
                    CurrentlyWorking = currentlyWorking,
                    OnLeave = totalStaff - currentlyWorking,
                    AverageWorkHoursToday = Math.Round(averageWorkHours, 2),
                    RecentShifts = todayShifts.Take(10).Select(s => new ShiftRecordDto
                    {
                        Id = s.Id,
                        UserId = s.UserId,
                        UserName = s.User?.Name ?? "Unknown",
                        CheckInTime = s.CheckInTime,
                        CheckOutTime = s.CheckOutTime,
                        Note = s.Note,
                        WorkDuration = s.CheckOutTime.HasValue ? s.CheckOutTime.Value - s.CheckInTime : null,
                        CreatedAt = s.CreatedAt
                    }).ToList()
                };

                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }
    }
} 
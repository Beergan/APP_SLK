using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuomRaCoffe.API.Data;
using RuomRaCoffe.API.Data.Entities;
using RuomRaCoffe.Shared.Dtos;
using RuomRaCoffe.API.Interfaces;


namespace RuomRaCoffe.API.Controllers
{
    [ApiController]
    [Route("api/login/[Controller]")]
    public class CheckUserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAuthService _auth;
        public CheckUserController(DataContext context, IPasswordHasher<User> passwordHasher,IAuthService auth)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _auth = auth;
        }
        [HttpPost("login")]

        public async Task<IActionResult> AdminLoginAsync([FromBody] LoginDto _checklogin)
        {
            try
            {

                if (_checklogin == null || string.IsNullOrEmpty(_checklogin.Email) || string.IsNullOrEmpty(_checklogin.Password))
                {
                    return BadRequest(new { error = "Invalid login request" });
                }
                var infor = await _context.Users.FirstOrDefaultAsync(x => x.Email == _checklogin.Email);
                if (infor == null)
                {
                    return NotFound(new { error = "Không tìm thấy user" });
                }
                var result = _passwordHasher.VerifyHashedPassword(infor, infor.Password!, _checklogin.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    return NotFound(new { error = "Sai mật khẩu" });
                }
               var token = await _auth.GenerateJwtTokenAsync(infor.Id.ToString(), infor.Email!, infor.Role!);
                return Ok(new LoggedInUser(infor.Id, infor.Name!, infor.Email!, token));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }


    }
} 
namespace RuomRaCoffe.API.Interfaces;
using RuomRaCoffe.Shared.Dtos.Login;

public interface IAuthService 
{
    Task<string> GenerateJwtTokenAsync(string userId, string userName, string role);
    Task<bool> ValidateUserAsync(string username, string password);
}

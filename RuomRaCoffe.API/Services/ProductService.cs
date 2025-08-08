using RuomRaCoffe.API.Data;
using RuomRaCoffe.API.Data.Entities;
using RuomRaCoffe.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace RuomRaCoffe.API.Services;

public class ProductService
{
    private readonly DataContext _context;

    public ProductService( DataContext context)
    {
        _context = context;
    }
    public async Task<ProductDto[]> GetProductsAsync() => await _context.Products
        .AsNoTracking().Select(x => new ProductDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            Discount = x.Discount,
            ImageUrl = x.ImageUrl,
            Stock = x.Stock,
            Category = x.Category,
            Brand = x.Brand,
            CategoryName = x.CategoryName
        }).ToArrayAsync();

}

public class AuthService
{
    private readonly DataContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(DataContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }
    public async Task<ApiResult> RegisterAsync(RegisterDto register)
    {
        if(await _context.Users.AnyAsync(x=>x.Email  == register.Email))
        {
            return ApiResult.ApiError("Email already exists");
        }
        var user = new User
        {
            Name = register.Name,
            Email = register.Email,
            Phone = register.Phone,
        };
        user.Password = _passwordHasher.HashPassword(user, register.Password);

        try { 
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return ApiResult.ApiSuccess();
        }
        catch(Exception ex)
        {
            return ApiResult.ApiError("An error occurred while registering the user: " + ex.Message);
        }



    }
}

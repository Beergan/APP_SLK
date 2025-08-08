using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RuomRaCoffe.API.Data.Entities;
using RuomRaCoffe.Shared.Dtos;

namespace RuomRaCoffe.Admin.Services;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("API");
    }

    public async Task<List<User>> GetUsersAsync()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<User>>("api/User");
            return result ?? new List<User>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to get users: {ex.Message}");
        }
    }

    public async Task<User> CreateUserAsync(User user)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/User", user);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<User>();
            return result ?? throw new Exception("API returned null for user creation.");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to create user: {ex.Message}");
        }
    }

}
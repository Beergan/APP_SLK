using System.Net.Http;
using System.Net.Http.Json;
using RuomRaCoffe.API.Data.Entities;
using RuomRaCoffe.Shared.Dtos;

namespace RuomRaCoffe.API.Services;

public class StaffService
{
    private readonly HttpClient _httpClient;

    public StaffService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("API");
    }

    // Staff Management
    public async Task<List<User>> GetStaffAsync()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<User>>("api/User/staff");
            return result ?? new List<User>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to get staff: {ex.Message}");
        }
    }

    public async Task<User> GetStaffByIdAsync(Guid id)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<User>($"api/User/{id}");
            return result ?? throw new Exception("Staff not found");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to get staff: {ex.Message}");
        }
    }

    public async Task<User> CreateStaffAsync(CreateStaffDto createStaffDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/staff", createStaffDto);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API returned {(int)response.StatusCode} ({response.ReasonPhrase}): {errorContent}");
            }

            var result = await response.Content.ReadFromJsonAsync<User>();
            return result ?? throw new Exception("API returned null for staff creation.");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to create staff: {ex.Message}");
        }
    }

    public async Task<User> UpdateStaffAsync(Guid id, UpdateStaffDto updateStaffDto)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/User/{id}", updateStaffDto);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<User>();
            return result ?? throw new Exception("API returned null for staff update.");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to update staff: {ex.Message}");
        }
    }

    public async Task DeleteStaffAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/User/{id}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to delete staff: {ex.Message}");
        }
    }

    public async Task<StaffStatisticsDto> GetStaffStatisticsAsync()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<StaffStatisticsDto>("api/User/staff/statistics");
            return result ?? new StaffStatisticsDto();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to get staff statistics: {ex.Message}");
        }
    }
} 
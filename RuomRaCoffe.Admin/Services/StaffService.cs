using System.Net.Http;
using System.Net.Http.Json;
using RuomRaCoffe.Shared.Dtos;

namespace RuomRaCoffe.Admin.Services;

public class StaffService
{
    private readonly HttpClient _httpClient;

    public StaffService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("API");
    }

    public async Task<List<StaffDto>> GetStaffAsync()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<StaffDto>>("api/User/staff");
            return result ?? new List<StaffDto>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to get staff: {ex.Message}");
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

    // Shift Management
    

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
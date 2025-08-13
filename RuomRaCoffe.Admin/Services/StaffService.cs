using System.Net.Http;
using System.Net.Http.Json;
using RuomRaCoffe.API.Data.Entities;
using RuomRaCoffe.Shared.Dtos;

namespace RuomRaCoffe.Admin.Services;

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

    public async Task<User> CreateStaffAsync(User createStaffDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/staff", createStaffDto);

            // Nếu không thành công, đọc content để xem lý do
            if (!response.IsSuccessStatusCode)
            {
                // Dòng này sẽ chứa thông tin chi tiết về lỗi
                var errorContent = await response.Content.ReadAsStringAsync();
                // Hãy debug và xem giá trị của errorContent ở đây
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

    // Shift Management
    public async Task<List<ShiftRecord>> GetShiftRecordsAsync(DateTime? fromDate = null, DateTime? toDate = null, Guid? userId = null)
    {
        try
        {
            var queryParams = new List<string>();
            if (fromDate.HasValue)
                queryParams.Add($"fromDate={fromDate.Value:yyyy-MM-dd}");
            if (toDate.HasValue)
                queryParams.Add($"toDate={toDate.Value:yyyy-MM-dd}");
            if (userId.HasValue)
                queryParams.Add($"userId={userId.Value}");

            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            var result = await _httpClient.GetFromJsonAsync<List<ShiftRecord>>($"api/ShiftRecord{queryString}");
            return result ?? new List<ShiftRecord>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to get shift records: {ex.Message}");
        }
    }

    public async Task<ShiftRecord> CheckInAsync(Guid userId, string? note = null)
    {
        try
        {
            var checkInData = new CheckInOutDto { UserId = userId, Note = note };
            var response = await _httpClient.PostAsJsonAsync("api/ShiftRecord/checkin", checkInData);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ShiftRecord>();
            return result ?? throw new Exception("API returned null for check-in.");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to check in: {ex.Message}");
        }
    }

    public async Task<ShiftRecord> CheckOutAsync(Guid userId, string? note = null)
    {
        try
        {
            var checkOutData = new CheckInOutDto { UserId = userId, Note = note };
            var response = await _httpClient.PostAsJsonAsync("api/ShiftRecord/checkout", checkOutData);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ShiftRecord>();
            return result ?? throw new Exception("API returned null for check-out.");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to check out: {ex.Message}");
        }
    }

    public async Task<ShiftRecord> UpdateShiftRecordAsync(Guid id, ShiftRecord shiftRecord)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/ShiftRecord/{id}", shiftRecord);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ShiftRecord>();
            return result ?? throw new Exception("API returned null for shift record update.");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to update shift record: {ex.Message}");
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
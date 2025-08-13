namespace RuomRaCoffe.Shared.Dtos;

public class StaffDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastCheckIn { get; set; }
    public DateTime? LastCheckOut { get; set; }
    public bool IsCurrentlyWorking { get; set; }
}

public class CreateStaffDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

public class UpdateStaffDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public class CheckInOutDto
{
    public Guid UserId { get; set; }
    public string? Note { get; set; }
}

public class ShiftRecordDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public string? Note { get; set; }
    public TimeSpan? WorkDuration { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class StaffStatisticsDto
{
    public int TotalStaff { get; set; }
    public int CurrentlyWorking { get; set; }
    public int OnLeave { get; set; }
    public double AverageWorkHoursToday { get; set; }
    public List<ShiftRecordDto> RecentShifts { get; set; } = new();
} 
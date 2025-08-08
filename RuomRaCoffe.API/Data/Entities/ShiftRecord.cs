namespace RuomRaCoffe.API.Data.Entities;

public class ShiftRecord
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public User User { get; set; }
}
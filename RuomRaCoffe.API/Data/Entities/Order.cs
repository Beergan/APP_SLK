using System.ComponentModel.DataAnnotations;

namespace RuomRaCoffe.API.Data.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
    public string? Status { get; set; } = "Pending";

    public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
}


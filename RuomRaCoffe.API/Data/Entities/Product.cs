using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuomRaCoffe.API.Data.Entities;

public class Product 
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; } = 0; 
    public string? ImageUrl { get; set; }
    public int Stock { get; set; } = 0; 
    public string? Category { get; set; } 
    public string? Brand { get; set; } 
    public string? CategoryName { get; set; }
}


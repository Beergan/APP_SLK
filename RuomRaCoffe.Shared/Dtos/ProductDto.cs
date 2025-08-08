using System.ComponentModel.DataAnnotations;

namespace RuomRaCoffe.Shared.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string? ImageUrl { get; set; }
    public int Stock { get; set; }
    public string? Category { get; set; }
    public string? Brand { get; set; }
    public string? CategoryName { get; set; }
}

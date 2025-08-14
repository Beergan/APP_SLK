using System.ComponentModel.DataAnnotations;

namespace RuomRaCoffe.Shared.Dtos;

public class ProductDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string? ImageUrl { get; set; }
    public int StockQuantity { get; set; } // Đổi tên từ Stock thành StockQuantity
    public string? Category { get; set; }
    public string? Brand { get; set; }
    public string? CategoryName { get; set; }
    public bool IsActive { get; set; } = true; // Thêm trạng thái hoạt động
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}

// Thêm Product class để sử dụng trong POS
public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}

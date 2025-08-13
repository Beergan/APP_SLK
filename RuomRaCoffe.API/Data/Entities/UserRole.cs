using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuomRaCoffe.API.Data.Entities;

public class UserRole 
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public string? Code { get; set; }   
    
    public string? Description { get; set; }    
}


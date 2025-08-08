using System.ComponentModel.DataAnnotations;

namespace RuomRaCoffe.API.Data.Entities;

public class  UserRole 
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Code { get; set; }   
    public string? Description { get; set; }    

}


using System.ComponentModel.DataAnnotations;

namespace RuomRaCoffe.Shared.Dtos;

public class  RegisterDto
{
    public string Name { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email { get; set; }
    public string Phone { get; set; }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuomRaCoffe.Shared.Models;

public class UserModel
{
    public int Id { get; set; }
    [Required]

    public string? Name { get; set; }
    [Required]
    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Role { get; set; } = "User";

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public string? NameFull { get; set; }
}

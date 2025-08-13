using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace RuomRaCoffe.API.Data.Entities;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class User
    {[Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]

    public string ? Name { get; set; }
    [Required]
    public string ? Password { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Role { get; set; } = "User";

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public ICollection<UserRole>? UserRole { get; set; }

    public string ? NameFull { get; set; }
    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}

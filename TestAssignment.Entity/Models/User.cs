using System.ComponentModel.DataAnnotations.Schema;

namespace TestAssignment.Entity.Models;

public class User
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string? Phone { get; set; }

    public string Password { get; set; } = null!;

    public int Roleid { get; set; }

    [Column(TypeName= "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column(TypeName= "timestamp without time zone")]
    public DateTime? Updateat { get; set; }

    public string? PasswordResetToken { get; set; }

    [Column(TypeName= "timestamp without time zone")]
    public DateTime? ResetTokenExpiry { get; set; }

    public string? PasswordHash { get; set; }

    public bool Isdeleted { get; set; } = false;

    public virtual Role Role { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

}

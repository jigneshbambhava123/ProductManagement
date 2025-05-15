using System.ComponentModel.DataAnnotations.Schema;

namespace TestAssignment.Entity.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; } = null!;

    public decimal Rate { get; set; }

    public bool Isdeleted { get; set; } =false;

    [Column(TypeName= "timestamp without time zone")]
    public DateTime Createdat { get; set; }

    [Column(TypeName= "timestamp without time zone")]
    public DateTime Updateat { get; set; }

}

using System.ComponentModel.DataAnnotations.Schema;

namespace TestAssignment.Entity.Models;

public class Orderdetail
{
    public int Id { get; set; }

    public int Orderid { get; set; }

    public int Productid { get; set; }

    public int? Quantity { get; set; }

    public decimal? Unitprice { get; set; }

    public decimal Productamount { get; set; }

    public string? Specialinstruction { get; set; }

    public bool Isdeleted { get; set; } = false;

    [Column(TypeName= "timestamp without time zone")]
    public DateTime Updatedat { get; set; }

    [Column(TypeName= "timestamp without time zone")]
    public DateTime Createdat { get; set; }

    public int? Isprepared { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}

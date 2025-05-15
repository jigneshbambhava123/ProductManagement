using System.ComponentModel.DataAnnotations.Schema;

namespace TestAssignment.Entity.Models;

public class Order
{
    public int Id { get; set; }

    [Column(TypeName= "timestamp without time zone")]
    public DateTime Orderdate { get; set; }

    public int Userid { get; set; }

    public string Status { get; set; } = null!;

    public string? Paymentmode { get; set; }

    public decimal Orderamount { get; set; }

    public string? Orderinstructions { get; set; }

    public bool Isdeleted { get; set; }

    [Column(TypeName= "timestamp without time zone")]
    public DateTime Updatedat { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Paidamount { get; set; }

    public decimal? Rating { get; set; }

    [Column(TypeName= "timestamp without time zone")]
    public DateTime? Paidon { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

}

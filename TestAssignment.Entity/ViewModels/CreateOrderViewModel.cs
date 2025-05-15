using System.ComponentModel.DataAnnotations;

namespace TestAssignment.Entity.ViewModels;

public class CreateOrderViewModel
{
    [Required(ErrorMessage = "TotalAmount is required.")]
    public decimal TotalAmount { get; set; }

}

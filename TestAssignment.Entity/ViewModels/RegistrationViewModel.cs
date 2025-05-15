using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestAssignment.Entity.ViewModels;

public class RegistrationViewModel
{
    [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^\S.*\S$|^\S$", ErrorMessage = "First name cannot have leading or trailing spaces.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^\S.*\S$|^\S$", ErrorMessage = "Last name cannot have leading or trailing spaces.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^\S.*\S$|^\S$", ErrorMessage = "Username cannot have leading or trailing spaces.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 12 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")] // Optional: Enforce 10 digits
        public string Phone { get; set; }

        [Required(ErrorMessage = "Role selection is required")]
        public int RoleId { get; set; }

        public IEnumerable<SelectListItem>? Roles { get; set; }
}

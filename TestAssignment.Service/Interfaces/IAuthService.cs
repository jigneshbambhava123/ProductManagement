using TestAssignment.Entity.Models;
using TestAssignment.Entity.ViewModels;

namespace TestAssignment.Service.Interfaces;

public interface IAuthService
{
    Task<User> AuthenticateUserAsync(string email, string Password);
    Task<(bool success, string message)> AddNewUserAsync(RegistrationViewModel model);

}

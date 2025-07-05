using TestAssignment.Entity.Helpers;
using TestAssignment.Entity.Models;
using TestAssignment.Entity.ViewModels;
using TestAssignment.Repository.Interfaces;
using TestAssignment.Service.Interfaces;

namespace TestAssignment.Service.Implementations;
//authservice Iauthservice
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> AuthenticateUserAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if(user != null  && PasswordHasher.VerifyPassword(password,user.PasswordHash)){
            
            return user;
        }
        return null;
    }

    public async Task<(bool success, string message)> AddNewUserAsync(RegistrationViewModel model)
    {
        // Check if email already exists using your existing method
        var existingUser = await _userRepository.GetUserByEmailAsync(model.Email);
        
        if (existingUser != null)
        {
            return (false, "A user with this email already exists");
        }
        
        // Create user entity from view model
        var newUser = new User
        {
            Firstname = model.FirstName,
            Lastname = model.LastName,
            Username = model.UserName,
            Email = model.Email,
            Password = model.Password,
            Phone = model.Phone,
            Roleid = model.RoleId,
            Createdat = DateTime.Now,
            Updateat = DateTime.Now,
            Isdeleted = false
        };
        
        // Add these additional properties
        newUser.PasswordHash = PasswordHasher.HashPassword(newUser.Password);
        newUser.PasswordResetToken = Guid.NewGuid().ToString();
        newUser.ResetTokenExpiry = DateTime.Now.AddHours(1);
        
        // Use your existing repository method to add the user
        await _userRepository.AddNewUserAsync(newUser);
                
        return (true, "User Register successfully");
    }
}

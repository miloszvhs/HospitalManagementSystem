using HospitalManagementSystem.Domain.Abstractions;
using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Application.Services;

public class PasswordHasherService
{
    public string HashPassword(User user, string password)
    {
        var hashedPassword = "";
        
        return hashedPassword;
    }
    
    public bool ValidatePassword(User user, string password)
    {
        var hashedPassword = HashPassword(user, password);
        
        if()
        
        return true;
    }
}
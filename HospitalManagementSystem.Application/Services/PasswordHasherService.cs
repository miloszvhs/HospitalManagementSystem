using System.Security.Cryptography;
using System.Text;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Exceptions;
using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Application.Services;

public class PasswordHasherService
{
    public byte[] HashPassword(string password)
    {
        var bytePassword = Encoding.ASCII.GetBytes(password);
        
        using (var mySHA256 = SHA256.Create())
        {
            var hashedPassword = mySHA256.ComputeHash(bytePassword);
            return hashedPassword;
        }
    }
    
    public bool ValidatePassword(Employee employee, HospitalManagementSystemPassword password)
    {
        if (!password.Value.SequenceEqual(employee.Password.Value))
        {
            throw new InvalidPasswordException();
        }

        return true;
    }
}
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Interfaces;

public interface IPasswordHasherService
{
    byte[] HashPassword(string password);
    bool ValidatePassword(Employee employee, Password password);
}
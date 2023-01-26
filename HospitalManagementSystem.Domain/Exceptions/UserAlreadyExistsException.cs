using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class UserAlreadyExistsException : CustomException
{
    public UserAlreadyExistsException(string username) : base($"User with username:{username} already exists.")
    {
    }
}
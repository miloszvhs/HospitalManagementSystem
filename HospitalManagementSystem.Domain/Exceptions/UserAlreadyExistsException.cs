using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class UserAlreadyExistsException : CustomException
{
    public UserAlreadyExistsException() : base("User already exists.")
    {
    }
}
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class InvalidPasswordException : HospitalManagementSystemException
{
    public InvalidPasswordException() : base($"Invalid password")
    {
    }
}
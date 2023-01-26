using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class EmptyCustomPasswordException : CustomException
{
    public EmptyCustomPasswordException() : base($"Password cannot be empty.")
    {
    }
}
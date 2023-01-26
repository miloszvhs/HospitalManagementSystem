using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class EmptyCustomUsernameException : CustomException
{
    public EmptyCustomUsernameException() : base($"Username cannot be empty.")
    {
    }
}
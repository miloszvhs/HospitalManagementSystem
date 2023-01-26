using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class EmptyCustomIdException : CustomException
{
    public EmptyCustomIdException() : base($"Id cannot be empty.")
    {
    }
}
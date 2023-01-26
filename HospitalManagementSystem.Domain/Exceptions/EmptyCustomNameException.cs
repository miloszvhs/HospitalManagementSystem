using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class EmptyCustomNameException : CustomException
{
    public EmptyCustomNameException() : base($"Name cannot be empty or contain integers or special characters.")
    {
    }
}
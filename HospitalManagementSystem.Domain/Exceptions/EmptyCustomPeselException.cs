using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class EmptyCustomPeselException : CustomException
{
    public EmptyCustomPeselException() : base("Pesel is empty or contains white spaces")
    {
    }
}
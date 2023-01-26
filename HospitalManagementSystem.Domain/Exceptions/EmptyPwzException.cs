using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class EmptyPwzException : CustomException
{
    public EmptyPwzException() : base($"PWZ number cannot be empty.")
    {
    }
}
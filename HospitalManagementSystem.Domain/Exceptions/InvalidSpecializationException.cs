using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class InvalidSpecializationException : CustomException
{
    public InvalidSpecializationException() : base("Invalid specialization.")
    {
    }
}
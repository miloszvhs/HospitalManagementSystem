using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class InvalidPwzException : CustomException
{
    public InvalidPwzException(string value) : base($"PWZ {value} number is invalid.")
    {
    }
}
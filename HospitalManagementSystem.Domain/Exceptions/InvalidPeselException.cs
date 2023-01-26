using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class InvalidPeselException : CustomException
{
    public InvalidPeselException(string pesel) : base($"Pesel {pesel} is invalid. Contains letters or has different length than 11.")
    {
    }
}
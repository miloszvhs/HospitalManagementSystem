using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class InvalidHospitalManagementPeselException : HospitalManagementSystemException
{
    public InvalidHospitalManagementPeselException(string pesel) : base($"HMS Pesel {pesel} is invalid. Contains letters or has different length than 11.")
    {
    }
}
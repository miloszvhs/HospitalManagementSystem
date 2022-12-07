using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class InvalidHospitalManagementPWZException : HospitalManagementSystemException
{
    public InvalidHospitalManagementPWZException(string value) : base($"HMS PWZ {value} number is invalid.")
    {
    }
}
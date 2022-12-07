using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class EmptyHospitalManagementPWZException : HospitalManagementSystemException
{
    public EmptyHospitalManagementPWZException() : base($"HMS PWZ number cannot be empty.")
    {
    }
}
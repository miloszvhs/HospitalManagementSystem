using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class EmptyHospitalManagementSystemIdException : HospitalManagementSystemException
{
    public EmptyHospitalManagementSystemIdException() : base($"HMS Id cannot be empty.")
    {
    }
}
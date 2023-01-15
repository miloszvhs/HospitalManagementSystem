using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class EmptyHospitalManagementSystemNameException : HospitalManagementSystemException
{
    public EmptyHospitalManagementSystemNameException() : base($"HMS name cannot be empty or contain integers or special characters.")
    {
    }
}
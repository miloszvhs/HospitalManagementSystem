using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class EmptyHospitalManagementSystemPasswordException : HospitalManagementSystemException
{
    public EmptyHospitalManagementSystemPasswordException() : base($"HMS password cannot be empty.")
    {
    }
}
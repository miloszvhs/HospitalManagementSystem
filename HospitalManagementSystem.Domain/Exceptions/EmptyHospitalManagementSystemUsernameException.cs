using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class EmptyHospitalManagementSystemUsernameException : HospitalManagementSystemException
{
    public EmptyHospitalManagementSystemUsernameException() : base($"HMS Username cannot be empty.")
    {
    }
}
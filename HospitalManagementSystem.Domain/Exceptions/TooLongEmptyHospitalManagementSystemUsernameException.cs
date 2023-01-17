using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class TooLongEmptyHospitalManagementSystemUsernameException : HospitalManagementSystemException
{
    public TooLongEmptyHospitalManagementSystemUsernameException() : base("Username cannot be longer than 14 chars.")
    {
    }
}
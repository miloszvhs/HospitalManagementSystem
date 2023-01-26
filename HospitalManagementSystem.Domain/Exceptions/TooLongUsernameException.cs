using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class TooLongUsernameException : CustomException
{
    public TooLongUsernameException() : base("Username cannot be longer than 14 chars.")
    {
    }
}
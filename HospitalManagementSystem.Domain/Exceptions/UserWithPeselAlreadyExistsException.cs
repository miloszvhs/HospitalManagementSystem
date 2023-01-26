using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class UserWithPeselAlreadyExistsException : CustomException
{
    public UserWithPeselAlreadyExistsException(string pesel) : base($"User with pesel:{pesel} already exists.")
    {
    }
}
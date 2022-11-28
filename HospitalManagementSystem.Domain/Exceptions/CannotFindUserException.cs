using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class CannotFindUserException : HospitalManagementSystemException
{
    public CannotFindUserException(string user) : base($"Cannot find '{user}'.")
    {
    }
}
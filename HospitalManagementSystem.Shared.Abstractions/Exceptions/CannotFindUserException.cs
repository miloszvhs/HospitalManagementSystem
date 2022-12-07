namespace HospitalManagementSystem.Shared.Abstractions.Exceptions;

public class CannotFindUserException : HospitalManagementSystemException
{
    public CannotFindUserException(int userId) : base($"Cannot find user with ID: '{userId}'.")
    {
    }
}
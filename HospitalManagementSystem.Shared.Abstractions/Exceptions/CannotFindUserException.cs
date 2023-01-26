namespace HospitalManagementSystem.Shared.Abstractions.Exceptions;

public class CannotFindUserException : CustomException
{
    public CannotFindUserException(int userId) : base($"Cannot find user with ID: '{userId}'.")
    {
    }
    
    public CannotFindUserException(string userLogin) : base($"Cannot find user with username: '{userLogin}'.")
    {
    }
}
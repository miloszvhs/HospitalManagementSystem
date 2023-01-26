using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class UserWithIdDoesntExistException : CustomException
{
    public UserWithIdDoesntExistException(int id) : base($"User with Id:{id} doesnt exists.")
    {
    }
}
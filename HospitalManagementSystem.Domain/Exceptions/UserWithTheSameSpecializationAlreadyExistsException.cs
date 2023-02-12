using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class UserWithTheSameSpecializationAlreadyExistsException : CustomException
{
    public UserWithTheSameSpecializationAlreadyExistsException() : base("User with the same specialization already exists.")
    {
    }
}
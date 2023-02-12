using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class UserHasAlreadyMaximalNumberOfShiftsException: CustomException
{
    public UserHasAlreadyMaximalNumberOfShiftsException() : base("User has already maximal number of shifts.")
    {
    }
}
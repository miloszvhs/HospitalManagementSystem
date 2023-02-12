using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class UserHasAlreadyAShiftDayBeforeOrDayAfterException : CustomException
{
    public UserHasAlreadyAShiftDayBeforeOrDayAfterException() 
        : base("User has already a shift one day before or one day after.")
    {
    }
}
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class GivenDateIsBeforeCurrentDateException : CustomException
{
    public GivenDateIsBeforeCurrentDateException() : base("Cannot add shift because selected date is before current date.")
    {
    }
}
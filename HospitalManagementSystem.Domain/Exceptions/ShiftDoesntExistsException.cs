using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class ShiftDoesntExistsException : CustomException
{
    public ShiftDoesntExistsException() : base("There is no shift on this day.")
    {
    }
}
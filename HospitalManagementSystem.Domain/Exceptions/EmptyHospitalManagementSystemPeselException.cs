using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class EmptyHospitalManagementSystemPeselException : HospitalManagementSystemException
{
    public EmptyHospitalManagementSystemPeselException() : base("HMS Pesel is empty or contains white spaces")
    {
    }
}
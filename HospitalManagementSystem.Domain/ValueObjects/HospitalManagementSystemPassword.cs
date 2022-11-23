using HospitalManagementSystem.Domain.Exceptions;

namespace HospitalManagementSystem.Domain.ValueObjects;

public record HospitalManagementSystemPassword
{
    public string Value { get; }

    public HospitalManagementSystemPassword(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyHospitalManagementSystemPasswordException();
        }
        
        Value = value;
    }
}
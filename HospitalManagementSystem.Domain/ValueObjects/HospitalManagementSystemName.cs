using HospitalManagementSystem.Domain.Exceptions;

namespace HospitalManagementSystem.Domain.ValueObjects;

public record HospitalManagementSystemName
{
    public string Value { get; }

    public HospitalManagementSystemName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyHospitalManagementSystemNameException();
        }
        
        Value = value;
    }

    public static implicit operator string(HospitalManagementSystemName name)
        => name.Value;

    public static implicit operator HospitalManagementSystemName(string name)
        => new(name);
}
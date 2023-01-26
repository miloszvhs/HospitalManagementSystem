using HospitalManagementSystem.Domain.Exceptions;

namespace HospitalManagementSystem.Domain.ValueObjects;

public record Name
{
    public string Value { get; }

    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Any(char.IsLetter))
        {
            throw new EmptyCustomNameException();
        }
        
        Value = value;
    }

    public static implicit operator string(Name name)
        => name.Value;

    public static implicit operator Name(string name)
        => new(name);
}
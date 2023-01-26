using HospitalManagementSystem.Domain.Exceptions;

namespace HospitalManagementSystem.Domain.ValueObjects;

public record Pesel
{
    public string Value { get; }

    public Pesel(string value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCustomPeselException();
        }

        if (value.Any(char.IsLetter) || value.Length != 11)
        {
            throw new InvalidPeselException(value);
        }

        Value = value;
    }

    public static implicit operator string(Pesel pesel)
        => pesel.Value;
    
    public static implicit operator Pesel(string pesel)
        => new(pesel);
}
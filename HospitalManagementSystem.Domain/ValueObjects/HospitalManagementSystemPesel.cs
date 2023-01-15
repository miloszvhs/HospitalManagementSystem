using HospitalManagementSystem.Domain.Exceptions;

namespace HospitalManagementSystem.Domain.ValueObjects;

public record HospitalManagementSystemPesel
{
    public string Value { get; }

    public HospitalManagementSystemPesel(string value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyHospitalManagementSystemPeselException();
        }

        if (value.Any(char.IsLetter) || value.Length != 11)
        {
            throw new InvalidHospitalManagementPeselException(value);
        }

        Value = value;
    }

    public static implicit operator string(HospitalManagementSystemPesel pesel)
        => pesel.Value;
    
    public static implicit operator HospitalManagementSystemPesel(string pesel)
        => new(pesel);
}
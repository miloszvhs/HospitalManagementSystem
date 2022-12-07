using HospitalManagementSystem.Domain.Exceptions;

namespace HospitalManagementSystem.Domain.ValueObjects;

public record HospitalManagementSystemPWZ
{
    public int Value { get; }

    public HospitalManagementSystemPWZ(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            throw new EmptyHospitalManagementPWZException();
        }
        
        if(value.Length != 8 || value.Count(Char.IsLetter) > 1 || !Char.IsLetter(value.Last()) || value.First() == 0)
        {
            throw new InvalidHospitalManagementPWZException(value);
        }
        
        Value = int.Parse(value);
    }
    
    public static implicit operator int(HospitalManagementSystemPWZ pwz)
        => pwz.Value;

    public static implicit operator HospitalManagementSystemPWZ(string pwz)
        => new(pwz);
}
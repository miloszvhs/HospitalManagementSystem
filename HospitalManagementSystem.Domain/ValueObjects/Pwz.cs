using HospitalManagementSystem.Domain.Exceptions;

namespace HospitalManagementSystem.Domain.ValueObjects;

public record Pwz
{
    public int Value { get; }

    public Pwz(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            throw new EmptyPwzException();
        }
        
        if(value.Length != 7 || value.Count(Char.IsLetter) > 0 || value.First() == 0)
        {
            throw new InvalidPwzException(value);
        }
        
        Value = int.Parse(value);
    }
    
    public static implicit operator int(Pwz pwz)
        => pwz.Value;

    public static implicit operator Pwz(string pwz)
        => new(pwz);
}
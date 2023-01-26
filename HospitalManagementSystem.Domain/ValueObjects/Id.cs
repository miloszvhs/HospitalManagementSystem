using HospitalManagementSystem.Domain.Exceptions;

namespace HospitalManagementSystem.Domain.ValueObjects;

public record Id
{
    public int Value { get; }

    public Id(int value)
    {
        if(value < 0)
        {
            throw new EmptyCustomIdException();
        }
        
        Value = value;
    }

    public static implicit operator int(Id id)
        => id.Value;

    public static implicit operator Id(int id)
        => new(id);
}
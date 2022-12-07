using HospitalManagementSystem.Domain.Exceptions;

namespace HospitalManagementSystem.Domain.ValueObjects;

public record HospitalManagementSystemId
{
    public int Value { get; }

    public HospitalManagementSystemId(int value)
    {
        if(value < 0)
        {
            throw new EmptyHospitalManagementSystemIdException();
        }
        
        Value = value;
    }

    public static implicit operator int(HospitalManagementSystemId id)
        => id.Value;

    public static implicit operator HospitalManagementSystemId(int id)
        => new(id);
}
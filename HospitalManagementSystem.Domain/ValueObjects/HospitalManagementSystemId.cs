using HospitalManagementSystem.Domain.Exceptions;

namespace HospitalManagementSystem.Domain.ValueObjects;

public record HospitalManagementSystemId
{
    public Guid Value { get; }

    public HospitalManagementSystemId(Guid value)
    {
        if(value == Guid.Empty)
        {
            throw new EmptyHospitalManagementSystemIdException();
        }
        
        Value = value;
    }

    public static implicit operator Guid(HospitalManagementSystemId id)
        => id.Value;

    public static implicit operator HospitalManagementSystemId(Guid id)
        => new(id);
}
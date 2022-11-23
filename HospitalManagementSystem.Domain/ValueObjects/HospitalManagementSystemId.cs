namespace HospitalManagementSystem.Domain.ValueObjects;

public record HospitalManagementSystemId
{
    public Guid Value { get; }

    public HospitalManagementSystemId(Guid value)
    {
        Value = value;
    }

    public static implicit operator Guid(HospitalManagementSystemId id)
        => id.Value;

    public static implicit operator HospitalManagementSystemId(Guid id)
        => new(id);
}
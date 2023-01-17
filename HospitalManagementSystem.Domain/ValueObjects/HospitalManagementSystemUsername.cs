using HospitalManagementSystem.Domain.Exceptions;

namespace HospitalManagementSystem.Domain.ValueObjects;

public record HospitalManagementSystemUsername
{
    public string Value { get; }

    public HospitalManagementSystemUsername(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyHospitalManagementSystemUsernameException();
        }
        
        if(value.Length > 14)
        {
            throw new TooLongEmptyHospitalManagementSystemUsernameException();
        }
        
        Value = value;
    }

    public static implicit operator string(HospitalManagementSystemUsername username)
        => username.Value;

    public static implicit operator HospitalManagementSystemUsername(string username)
        => new(username);

}
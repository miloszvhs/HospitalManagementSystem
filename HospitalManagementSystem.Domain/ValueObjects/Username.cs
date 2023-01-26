using HospitalManagementSystem.Domain.Exceptions;

namespace HospitalManagementSystem.Domain.ValueObjects;

public record Username
{
    public string Value { get; }

    public Username(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCustomUsernameException();
        }
        
        if(value.Length > 14)
        {
            throw new TooLongUsernameException();
        }
        
        Value = value;
    }

    public static implicit operator string(Username username)
        => username.Value;

    public static implicit operator Username(string username)
        => new(username);

}
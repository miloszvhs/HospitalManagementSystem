using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Abstractions;

public class User
{
    public HospitalManagementSystemUsername Username { get; }
    public HospitalManagementSystemPassword Password { get; }

    public User(HospitalManagementSystemUsername username,
        HospitalManagementSystemPassword password)
    {
        Username = username;
        Password = password;
    }
}
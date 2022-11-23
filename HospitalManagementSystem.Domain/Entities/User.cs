using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Entities;

public class User
{
    public HospitalManagementSystemId Id { get; }
    public HospitalManagementSystemUsername Username { get; }
    public HospitalManagementSystemPassword Password { get; }

    public User(HospitalManagementSystemId id,
        HospitalManagementSystemUsername username,
        HospitalManagementSystemPassword password)
    {
        Id = id;
        Username = username;
        Password = password;
    }
}
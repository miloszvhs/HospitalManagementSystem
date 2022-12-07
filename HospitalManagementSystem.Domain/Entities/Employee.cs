using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Entities;

public class Employee
{
    public HospitalManagementSystemId Id { get; }
    public HospitalManagementSystemName Name { get; }
    public HospitalManagementSystemName LastName { get; }
    public HospitalManagementSystemUsername Username { get; }
    public HospitalManagementSystemPassword Password { get; }
    
    public Employee(HospitalManagementSystemUsername username, HospitalManagementSystemPassword password)
    {
        Username = username;
        Password = password;
    }
    
    public Employee(HospitalManagementSystemUsername username,
        HospitalManagementSystemPassword password,
        HospitalManagementSystemId id,
        HospitalManagementSystemName name,
        HospitalManagementSystemName lastName)
    {
        Username = username;
        Password = password;
        Name = name;
        Id = id;
        LastName = lastName;
    }

    
}
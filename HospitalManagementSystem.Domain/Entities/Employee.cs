using HospitalManagementSystem.Domain.Abstractions;
using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Entities;

public class Employee : User
{
    public HospitalManagementSystemId Id { get; }
    public HospitalManagementSystemName Name { get; }
    public HospitalManagementSystemName LastName { get; }
    
    public Employee(HospitalManagementSystemUsername username, HospitalManagementSystemPassword password,
        HospitalManagementSystemId id,
        HospitalManagementSystemName name,
        HospitalManagementSystemName lastName) : base(username, password)
    {
        Name = name;
        Id = id;
        LastName = lastName;
    }
}
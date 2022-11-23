using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Entities;

public class Employee
{
    public HospitalManagementSystemId PersonalId { get; }
    public HospitalManagementSystemName Name { get; }
    public HospitalManagementSystemLastname Lastname { get; }
    public HospitalManagementSystemUsername Username { get; }
    public HospitalManagementSystemPassword Password { get; }

    public Employee(HospitalManagementSystemId personalId,
        HospitalManagementSystemName name,
        HospitalManagementSystemLastname lastname,
        HospitalManagementSystemUsername username,
        HospitalManagementSystemPassword password)
    {
        PersonalId = personalId;
        Name = name;
        Lastname = lastname;
        Username = username;
        Password = password;
    }
}
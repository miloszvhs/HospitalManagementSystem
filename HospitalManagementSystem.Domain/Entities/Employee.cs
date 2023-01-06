using HospitalManagementSystem.Domain.ValueObjects;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Entities;

public class Employee : BaseEntity
{
    public HospitalManagementSystemName Name { get; }
    public HospitalManagementSystemName LastName { get; }
    public HospitalManagementSystemUsername Username { get; }
    public HospitalManagementSystemPassword Password { get; }
    public Role Rola { get; init; }
    public DoctorPrivileges DoctorPrivileges { get; set; }

    public Employee()
    {
    }
    
    public Employee(HospitalManagementSystemUsername username, HospitalManagementSystemPassword password)
    {
        Username = username;
        Password = password;
    }
    
    public Employee(HospitalManagementSystemUsername username,
        HospitalManagementSystemPassword password,
        HospitalManagementSystemId id,
        HospitalManagementSystemName name,
        HospitalManagementSystemName lastName,
        Role rola) : this(username, password)
    {
        Name = name;
        Id = id;
        LastName = lastName;
        Rola = rola;
    }

    public Employee(HospitalManagementSystemUsername username,
        HospitalManagementSystemPassword password,
        HospitalManagementSystemId id,
        HospitalManagementSystemName name,
        HospitalManagementSystemName lastName,
        Role rola,
        DoctorPrivileges doctorPrivileges) : this(username, password, id, name, lastName, rola)
    {
        DoctorPrivileges = doctorPrivileges;
    }
}

public enum Role
{
    Pracownik,
    Lekarz,
    Administrator
}
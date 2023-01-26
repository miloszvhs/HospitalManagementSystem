using HospitalManagementSystem.Domain.ValueObjects;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Entities;

public class Employee : BaseEntity
{
    public Name Name { get; }
    public Name LastName { get; }
    public Username Username { get; }
    public Password Password { get; }
    public Pesel Pesel { get; }
    public Role Role { get; }
    public DoctorPrivileges? DoctorPrivileges { get; set; }

    public Employee()
    {
    }
    
    public Employee(Username username, Password password)
    {
        Username = username;
        Password = password;
    }
    
    public Employee(Username username,
        Password password,
        Pesel pesel,
        Id id,
        Name name,
        Name lastName,
        Role role) : this(username, password)
    {
        Pesel = pesel;
        Name = name;
        Id = id;
        LastName = lastName;
        Role = role;
    }

    public Employee(Username username,
        Password password,
        Pesel pesel,
        Id id,
        Name name,
        Name lastName,
        Role role,
        DoctorPrivileges doctorPrivileges) : this(username, password, pesel, id, name, lastName, role)
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
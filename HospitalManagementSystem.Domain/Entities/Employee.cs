using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Entities;

public class Employee
{
    
    public HospitalManagementSystemName Name { get; }
    public HospitalManagementSystemName Lastname { get; }


    public Employee(HospitalManagementSystemName name,
        HospitalManagementSystemName lastname)
    {
        Name = name;
        Lastname = lastname;
    }
}
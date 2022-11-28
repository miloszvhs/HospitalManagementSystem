using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Entities;

public class Doctor : Employee
{
    
    public Doctor(HospitalManagementSystemUsername username, HospitalManagementSystemPassword password, HospitalManagementSystemId id, HospitalManagementSystemName name, HospitalManagementSystemName lastName) : base(username, password, id, name, lastName)
    {
    }
}
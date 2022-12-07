using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Entities;

public class Admin : Employee
{
    public Admin(HospitalManagementSystemUsername username, 
        HospitalManagementSystemPassword password) : base(username, password)
    {
    }

    public Admin(HospitalManagementSystemUsername username,
        HospitalManagementSystemPassword password,
        HospitalManagementSystemId id, 
        HospitalManagementSystemName name,
        HospitalManagementSystemName lastName) : base(username, password, id, name, lastName)
    {
    }
}
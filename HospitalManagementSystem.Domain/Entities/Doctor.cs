using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Entities;

public class Doctor : Employee
{
    public HospitalManagementSystemPWZ Pwz { get; }

    public Doctor(HospitalManagementSystemUsername username,
        HospitalManagementSystemPassword password,
        HospitalManagementSystemId id,
        HospitalManagementSystemName name,
        HospitalManagementSystemName lastName,
        HospitalManagementSystemPWZ pwz
        ) : base(username, password, id, name, lastName)
    {
        Pwz = pwz;
    }
}
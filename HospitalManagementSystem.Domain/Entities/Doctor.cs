using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Entities;

public class Doctor : Employee
{
    public HospitalManagementSystemPWZ Pwz { get; }
    public Specjalizacja Specjalizacja { get; }

    public Doctor(HospitalManagementSystemUsername username,
        HospitalManagementSystemPassword password) : base(username, password)
    {
    }
    
    public Doctor(HospitalManagementSystemUsername username,
        HospitalManagementSystemPassword password,
        HospitalManagementSystemId id,
        HospitalManagementSystemName name,
        HospitalManagementSystemName lastName,
        HospitalManagementSystemPWZ pwz,
        Specjalizacja specjalizacja
        ) : base(username, password, id, name, lastName)
    {
        Pwz = pwz;
        Specjalizacja = specjalizacja;
        Rola = Role.Lekarz;
    }
}

public enum Specjalizacja
{
    Kardiolog = 1,
    Urolog,
    Laryngolog,
    Neurolog
}
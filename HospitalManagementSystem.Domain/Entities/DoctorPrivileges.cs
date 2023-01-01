using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Entities;

public class DoctorPrivileges
{
    public HospitalManagementSystemPWZ Pwz { get; }
    public Specjalizacja Specjalizacja { get; }

    public DoctorPrivileges()
    {
        
    }

    public DoctorPrivileges(HospitalManagementSystemPWZ pwz, 
        Specjalizacja specjalizacja)
    {
        Pwz = pwz;
        Specjalizacja = specjalizacja;
    }
}

public enum Specjalizacja
{
    Kardiolog = 1,
    Urolog,
    Laryngolog,
    Neurolog
}
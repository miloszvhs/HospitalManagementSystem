using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Entities;

public class DoctorPrivileges
{
    public Pwz Pwz { get; }
    public DoctorSpecialization DoctorSpecialization { get; }

    public DoctorPrivileges()
    {
        
    }

    public DoctorPrivileges(Pwz pwz, 
        DoctorSpecialization doctorSpecialization)
    {
        Pwz = pwz;
        DoctorSpecialization = doctorSpecialization;
    }
}

public enum DoctorSpecialization
{
    Kardiolog = 1,
    Urolog,
    Laryngolog,
    Neurolog
}
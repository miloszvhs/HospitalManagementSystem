using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Application.Services;

public class DoctorDatabaseService : HospitalManagementSystemBaseDb<Doctor>, IDatabaseService<Doctor>
{
    private readonly XMLService<Doctor> _xmlService;

    public DoctorDatabaseService()
    {
        _xmlService = new(this, "doctors.xml", "Doctors");
    }
    
    public void RestoreFromXmlFile()
    {
        _xmlService.RestoreFromXmlFile();
    }
    
    public void SaveToXmlFile()
    {
        _xmlService.SaveToXmlFile();
    }
}
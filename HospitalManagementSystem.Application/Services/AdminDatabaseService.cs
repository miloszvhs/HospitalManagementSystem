using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Application.Services;

public class AdminDatabaseService : HospitalManagementSystemBaseDb<Admin>, IDatabaseService<Admin>
{
    private readonly XMLService<Admin> _xmlService;

    public AdminDatabaseService()
    {
        _xmlService = new(this, "admins.xml", "Admins");
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
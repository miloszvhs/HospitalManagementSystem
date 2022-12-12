using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Application.Services;

public class EmployeeDatabaseService : HospitalManagementSystemBaseDb<Employee>, IDatabaseService<Employee>
{
    private readonly XMLService<Employee> _xmlService;

    public EmployeeDatabaseService()
    {
        _xmlService = new(this, "employees.xml", "Employees");
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
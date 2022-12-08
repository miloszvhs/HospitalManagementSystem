using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Application.Services;

public class EmployeeDatabaseService : HospitalManagementSystemBaseDb<Employee>
{
    private readonly XMLService<Employee> _xmlService;

    public EmployeeDatabaseService()
    {
        _xmlService = new(this, "employees.xml");
    }
}
using System.Xml.Serialization;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Infrastructure.Database;

namespace HospitalManagementSystem.Application.Services;

public class DatabaseService
{
    private readonly HospitalManagementSystemDb _database;
    private const string PATH = "hospitalManagementSystemDatabase.xml"; 

    public DatabaseService()
    {
    }

    public List<Employee> GetEmployees()
    {
        return _database.Employees;
    }
    
    public int GetLastId()
    {
        if(_database.Employees.Any())
        {
            var id = _database.Employees.OrderBy(x => x.Id).LastOrDefault().Id;
            return id;
        }

        return 0;
    }
}
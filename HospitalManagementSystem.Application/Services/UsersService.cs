using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Infrastructure.Database;

namespace HospitalManagementSystem.Application.Services;

internal sealed class UsersService
{
    private readonly HospitalManagementSystemDb _database;

    public UsersService(HospitalManagementSystemDb database)
    {
        _database = database;
    }

    public Employee GetEmployee(int id)
    {
        var employee = _database.Employees.FirstOrDefault(x => x.Id == id);

        if (employee is not null)
        {
            return employee;
        }

        return null;
    }
}
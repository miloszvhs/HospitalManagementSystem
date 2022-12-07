using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Exceptions;
using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Infrastructure.Database;

public class HospitalManagementSystemDb
{
    public IEnumerable<Employee> Employees => _employees;

    private readonly HashSet<Employee> _employees = new();

    public void AddUser(Employee employee) => _employees.Add(employee);
    public void UpdateUser(Employee employee)
    {
        var entity = _employees.FirstOrDefault(x => x.Id == employee.Id);

        if (entity is not null)
        {
            entity = employee;
        }
        else
        {
            throw new CannotFindUserException(employee.Id.ToString());
        }
    }

    public void RemoveUser(Employee employee)
    {
        var entity = _employees.FirstOrDefault(x => x.Username == employee.Username);

        if (entity is not null)
        {
            _employees.Remove(entity);
        }
        else
        {
            throw new CannotFindUserException(employee.Username);
        }
    }

    public void Seed()
    {
        _employees.Add(new Employee(new HospitalManagementSystemUsername("test"),
            new HospitalManagementSystemPassword(new byte[3] {15, 15, 13}),
            new HospitalManagementSystemId(1567),
            new HospitalManagementSystemName("name"),
            new HospitalManagementSystemName("lastname")));
    }
}
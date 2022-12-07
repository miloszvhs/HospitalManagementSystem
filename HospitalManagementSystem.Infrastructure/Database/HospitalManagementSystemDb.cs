using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Exceptions;
using HospitalManagementSystem.Domain.ValueObjects;
using HospitalManagementSystem.Shared.Abstractions.Exceptions;

namespace HospitalManagementSystem.Infrastructure.Database;

public class HospitalManagementSystemDb
{
    public List<Employee> Employees;

    public HospitalManagementSystemDb(List<Employee> employees)
    {
        Employees = employees;
    }

    public void AddEmployee(Employee employee) => Employees.Add(employee);
    public void UpdateEmployee(Employee employee)
    {
        var entity = Employees.FirstOrDefault(x => x.Id == employee.Id);

        if (entity is not null)
        {
            entity = employee;
        }
        else
        {
            throw new CannotFindUserException(employee.Id);
        }
    }

    public void RemoveEmployee(Employee employee)
    {
        var entity = Employees.FirstOrDefault(x => x.Username == employee.Username);

        if (entity is not null)
        {
            Employees.Remove(entity);
        }
        else
        {
            throw new CannotFindUserException(employee.Username);
        }
    }

    public Employee GetEmployee(int id)
    {
        var employee = Employees.FirstOrDefault(x => x.Id == id);
        
        if(employee is not null)
        {
            return employee;
        }

        return null;
    }

    public void Seed()
    {
        Employees.Add(new Employee(new HospitalManagementSystemUsername("test"),
            new HospitalManagementSystemPassword(new byte[3] {15, 15, 13}),
            new HospitalManagementSystemId(1567),
            new HospitalManagementSystemName("name"),
            new HospitalManagementSystemName("lastname")));
    }
}
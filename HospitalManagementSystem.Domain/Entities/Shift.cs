using HospitalManagementSystem.Domain.Exceptions;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Entities;

public class Shift : BaseEntity
{
    public DateTime Date { get; }
    public List<Employee> Users { get; } = new();

    public Shift()
    {
    }
    
    public Shift(DateTime date)
    {
        Date = date;
    }
    
    public void AddEmployee(Employee employee)
    {
        var user = Users.Any(x => x.Id == employee.Id);

        if (user)
        {
            throw new UserAlreadyExistsException();
        }

        if(employee.Role == Role.Lekarz)
        {
            var userWithTheSameSpecialization = Users.Where(x => x.Role == Role.Lekarz)
                .Any(x => x.DoctorPrivileges.DoctorSpecialization == employee.DoctorPrivileges.DoctorSpecialization);
        
            if(userWithTheSameSpecialization)
            {
                throw new UserWithTheSameSpecializationAlreadyExistsException();
            }
        }
        
        Users.Add(employee);
    }
    
    public void RemoveEmployee(int id)
    {
        var employee = Users.FirstOrDefault(x => x.Id == id);
        
        if(employee is null)
        {
            throw new UserWithIdDoesntExistException(id);
        }
        
        Users.Remove(employee);
    }
}
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.ValueObjects;
using HospitalManagementSystem.Infrastructure.Database;

namespace HospitalManagementSystem.Application.Services;

public class EmployeeLoginService
{
    private readonly DatabaseService _database;
    private readonly PasswordHasherService _passwordHasherService;

    public EmployeeLoginService(DatabaseService database,
        PasswordHasherService passwordHasherService)
    {
        _database = database;
        _passwordHasherService = passwordHasherService;
    }
    
    public Employee Login()
    {
        var usernameInput = WriteAndRead("Login: ");
        
        HospitalManagementSystemUsername login = new(usernameInput);

        var passwordInput = WriteAndRead("Password: ");

        HospitalManagementSystemPassword userPassword = new(_passwordHasherService.HashPassword(passwordInput));
        
        var employee = new Employee(login, userPassword);

        var succeeded = _passwordHasherService.ValidatePassword(employee, passwordInput);

        if(succeeded)
        {
            return employee;
        }

        return null;
    }

    private string WriteAndRead(string value)
    {
        Console.Write(value);
        
        var result = Console.ReadLine();
        
        return result;
    }
}

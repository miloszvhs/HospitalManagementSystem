using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.ValueObjects;
using HospitalManagementSystem.Infrastructure.Database;
using HospitalManagementSystem.Shared.Abstractions.Exceptions;

namespace HospitalManagementSystem.Application.Services;

public class LoginService
{
    private readonly EmployeeDatabaseService _database;
    private readonly PasswordHasherService _passwordHasherService;

    public LoginService(EmployeeDatabaseService database,
        PasswordHasherService passwordHasherService)
    {
        _database = database;
        _passwordHasherService = passwordHasherService;
    }
    
    public Employee Login()
    {
        var succeeded = false;
        
        var usernameInput = WriteAndRead("Login: ");
        
        HospitalManagementSystemUsername userLogin = new(usernameInput);

        var passwordInput = WriteAndRead("Password: ");

        HospitalManagementSystemPassword userPassword = new(_passwordHasherService.HashPassword(passwordInput));

        var employee = _database.Users.FirstOrDefault(x => x.Username == userLogin);

        try
        {
            if (employee is null)
            {
                throw new CannotFindUserException(userLogin);
            }
        
            succeeded = _passwordHasherService.ValidatePassword(employee, passwordInput);

        }
        catch (Exception e)
        {
            Console.WriteLine($"Wystąpił problem z logowaniem:\n{e.Message}");
        }

        if(succeeded)
        {
            Console.WriteLine("Pomyślnie zalogowano!");
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

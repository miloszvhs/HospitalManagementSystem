using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Domain.ValueObjects;
using HospitalManagementSystem.Shared.Abstractions.Exceptions;

namespace HospitalManagementSystem.Application.Services;

public class LoginService : ILoginService
{
    private readonly IDatabaseService _database;
    private readonly IPasswordHasherService _passwordHasherService;

    public LoginService(IDatabaseService database,
        IPasswordHasherService passwordHasherService)
    {
        _passwordHasherService = passwordHasherService;
        _database = database;
    }

    public Employee Login()
    {
        var succeeded = false;

        var usernameInput = WriteAndRead("Login: ");

        HospitalManagementSystemUsername userLogin = new(usernameInput);

        var passwordInput = WriteAndRead("Password: ");

        HospitalManagementSystemPassword userPassword = new(_passwordHasherService.HashPassword(passwordInput));

        var employee = _database.Items.FirstOrDefault(x => x.Username == userLogin);

        try
        {
            if (employee is null) throw new CannotFindUserException(userLogin);

            succeeded = _passwordHasherService.ValidatePassword(employee, userPassword);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Wystąpił problem z logowaniem:\n{e.Message}");
        }

        if (succeeded)
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
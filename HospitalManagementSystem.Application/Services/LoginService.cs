using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Domain.ValueObjects;
using HospitalManagementSystem.Shared.Abstractions.Exceptions;

namespace HospitalManagementSystem.Application.Services;

public class LoginService
{
    private readonly IDatabaseService<Admin> _adminDatabase;
    private readonly IDatabaseService<Doctor> _doctorDatabase;
    private readonly IDatabaseService<Employee> _employeeDatabase;
    private readonly PasswordHasherService _passwordHasherService;

    public LoginService(IDatabaseService<Employee> employeeDatabase,
        IDatabaseService<Doctor> doctorDatabase,
        IDatabaseService<Admin> adminDatabase,
        PasswordHasherService passwordHasherService)
    {
        _passwordHasherService = passwordHasherService;
        _employeeDatabase = employeeDatabase;
        _doctorDatabase = doctorDatabase;
        _adminDatabase = adminDatabase;

        _database.AddRange(employeeDatabase.Users);
        _database.AddRange(doctorDatabase.Users);
        _database.AddRange(adminDatabase.Users);
    }

    private List<Employee> _database { get; } = new();

    public Employee Login()
    {
        var succeeded = false;

        var usernameInput = WriteAndRead("Login: ");

        HospitalManagementSystemUsername userLogin = new(usernameInput);

        var passwordInput = WriteAndRead("Password: ");

        HospitalManagementSystemPassword userPassword = new(_passwordHasherService.HashPassword(passwordInput));

        var employee = _database.FirstOrDefault(x => x.Username == userLogin);

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
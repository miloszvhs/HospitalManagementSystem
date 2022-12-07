using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.ValueObjects;
using HospitalManagementSystem.Infrastructure.Database;

namespace HospitalManagementSystem.Application.Services;

public class EmployeeRegistrationService
{
    private readonly DatabaseService _database;
    private readonly PasswordHasherService _passwordHasherService;

    public EmployeeRegistrationService(DatabaseService database,
        PasswordHasherService passwordHasherService)
    {
        _database = database;
        _passwordHasherService = passwordHasherService;
    }

    public Employee Register()
    {
        Employee employee;
        var name = Console.ReadLine();
        var lastName = Console.ReadLine();
        var username = Console.ReadLine();
        var password = _passwordHasherService.HashPassword(Console.ReadLine());

        try
        {
            employee = new(
                new HospitalManagementSystemUsername(username),
                new HospitalManagementSystemPassword(password),
                new HospitalManagementSystemId(_database.GetLastId() + 1),
                new HospitalManagementSystemName(name),
                new HospitalManagementSystemName(lastName));
        }
        catch (Exception e)
        {
            Console.WriteLine($"Cos poszlo nie tak z rejestracją pracownika: \n{e.Message}");
            return null;
        }

        Console.WriteLine($"Pomyslnie stworzono pracownika {employee.Username}");
        return employee;
    }
}
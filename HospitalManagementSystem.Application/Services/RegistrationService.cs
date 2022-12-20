using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Application.Services;

public class RegistrationService
{
    private readonly IDatabaseService<Employee> _database;
    private readonly PasswordHasherService _passwordHasherService;

    public RegistrationService(IDatabaseService<Employee> database,
        PasswordHasherService passwordHasherService)
    {
        _database = database;
        _passwordHasherService = passwordHasherService;
    }

    public Employee Register()
    {
        Employee employee;
        Console.Write("Imię: ");
        var name = Console.ReadLine();
        Console.Write("Nazwisko: ");
        var lastName = Console.ReadLine();
        Console.Write("Nazwa użytkownika: ");
        var username = Console.ReadLine();
        Console.Write("Hasło: ");
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

        Console.WriteLine($"Pomyslnie stworzono pracownika {employee.Username.Value}");
        _database.AddUser(employee);
        _database.SaveToXmlFile();
        return employee;
    }
}
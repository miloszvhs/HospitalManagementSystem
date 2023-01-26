using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Application.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IDatabaseService _database;
    private readonly IPasswordHasherService _passwordHasherService;

    public RegistrationService(IDatabaseService database,
        IPasswordHasherService passwordHasherService)
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

        Console.Write("Pesel: ");
        var pesel = Console.ReadLine();

        Console.Write("Nazwa użytkownika: ");
        var username = Console.ReadLine();

        Console.Write("Hasło: ");
        var password = _passwordHasherService.HashPassword(Console.ReadLine());

        try
        {
            employee = new Employee(
                new Username(username),
                new Password(password),
                new Pesel(pesel),
                new Id(_database.GetLastId() + 1),
                new Name(name),
                new Name(lastName),
                Role.Pracownik);
            
            if (_database.Items.Find(x => x.Pesel == employee.Pesel) != null)
            {
                throw new Exception("Użytkownik z takim peselem już istnieje.");
            }
                    
            if(_database.Items.Find(x => x.Username == employee.Username) != null)
            {
                throw new Exception("Taki użytkownik już istnieje.");
            }
            
            Console.WriteLine($"Pomyslnie stworzono pracownika {employee.Username.Value}");
            _database.AddEmployee(employee);
            _database.SaveToXmlFile();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Cos poszlo nie tak z rejestracją pracownika: \n{e.Message}");
            return null;
        }

        return employee;
    }
}
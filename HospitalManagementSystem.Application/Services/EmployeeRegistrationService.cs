using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Infrastructure.Database;

namespace HospitalManagementSystem.Application.Services;

public class EmployeeRegistrationService
{
    private readonly HospitalManagementSystemDb _database;
    private readonly PasswordHasherService _passwordHasherService;

    public EmployeeRegistrationService(HospitalManagementSystemDb database,
        PasswordHasherService passwordHasherService)
    {
        _database = database;
        _passwordHasherService = passwordHasherService;
    }

    public Employee Register()
    {
        var name = Console.ReadLine();
        var lastName = Console.ReadLine();
        var username = Console.ReadLine();
        var password = Console.ReadLine();

        try
        {
            Employee employee = new();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        HospitalManagementSystemName Name     
        HospitalManagementSystemName LastName  
        HospitalManagementSystemUsername Username { get; }
        HospitalManagementSystemPassword Password { get; }
    }
}
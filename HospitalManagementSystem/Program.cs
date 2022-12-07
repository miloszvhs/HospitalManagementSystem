using System.Collections.Generic;
using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Infrastructure.Database;

internal class Program
{
    public static void Main()
    {
        var passwordService = new PasswordHasherService();
        var database = new DatabaseService();
        
        database.RestoreFromXMLFile();

        var registrationService = new EmployeeRegistrationService(database, passwordService);
        var loginService = new EmployeeLoginService(database, passwordService);
    }
}
using System.Collections.Generic;
using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Infrastructure.Database;

internal class Program
{
    public static void Main()
    {
        var database = new HospitalManagementSystemDb();
        var passwordService = new PasswordHasherService();
        
        var loginService = new EmployeeLoginService(database, passwordService);
    }
}
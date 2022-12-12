using System;
using System.Collections.Generic;
using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Infrastructure.Database;

internal class Program
{
    public static void Main()
    {
        var passwordService = new PasswordHasherService();
        var adminDatabase = new AdminDatabaseService();
        var doctorDatabase = new DoctorDatabaseService();
        var employeeDatabase = new EmployeeDatabaseService();
        
        adminDatabase.RestoreFromXmlFile();
        doctorDatabase.RestoreFromXmlFile();
        employeeDatabase.RestoreFromXmlFile();

        var registrationService = new EmployeeRegistrationService(employeeDatabase, passwordService);
        var loginService = new EmployeeLoginService(employeeDatabase, passwordService);

        loginService.Login();

    }
}
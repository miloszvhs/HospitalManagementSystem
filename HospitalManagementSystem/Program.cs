using System;
using System.Collections.Generic;
using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.ValueObjects;
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

        var registrationService = new RegistrationService(employeeDatabase, passwordService);
        var loginService = new LoginService(employeeDatabase, passwordService);
        var menuService = new MenuActionService();

        while (true)
        {
            menuService.DrawMenuViewByMenuType("MainMenu");
            
            var input = Console.ReadKey();
            Console.WriteLine();
            
            switch (input.KeyChar)
            {
                case '1':
                    if (loginService.Login() != null)
                    {
                        //naprawione
                    }
                    break;
                case '2':
                    registrationService.Register();
                    break;
                case '3':
                    return;
                default:
                    break;
            }
        }
         
        

    }
}
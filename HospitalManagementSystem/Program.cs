using System;
using System.Collections.Generic;
using HospitalManagementSystem.Application.Operations;
using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

internal class Program
{
    public static void Main()
    {
        var passwordService = new PasswordHasherService();
        var shiftService = new ShiftService();
        IDatabaseService<Admin> adminDatabase = new AdminDatabaseService();
        IDatabaseService<Doctor> doctorDatabase = new DoctorDatabaseService();
        IDatabaseService<Employee> employeeDatabase = new EmployeeDatabaseService();
        
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
                    Employee employee;
                    
                    if ((employee = loginService.Login()) != null)
                    {
                        switch (employee.Rola)
                        {
                            case Role.Administrator:
                                var adminOperations = new AdminOperations(menuService, adminDatabase, doctorDatabase, employeeDatabase, shiftService);
                                adminOperations.Run();
                                break;
                            case Role.Lekarz:
                                var doctorOperations = new DoctorOperations();
                                break;
                            case Role.Pracownik:
                                var employeeOperations = new EmployeeOperations();
                                break;
                            default:
                                Console.WriteLine("Niepoprawny typ");
                                break;
                        }
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
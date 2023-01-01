using System;
using HospitalManagementSystem.Application.Operations;
using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

internal class Program
{
    public static void Main()
    {
        IDatabaseService database = new DatabaseService();
        var shiftService = new ShiftService(database);
        var passwordService = new PasswordHasherService();

        database.RestoreFromXmlFile();

        var pwzNumberService = new PWZNumberService(database);
        var registrationService = new RegistrationService(database, passwordService);
        var loginService = new LoginService(database, passwordService);
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
                        switch (employee.Rola)
                        {
                            case Role.Administrator:
                                var adminOperations = new AdminOperations(menuService,
                                    shiftService,
                                    passwordService,
                                    pwzNumberService,
                                    database);

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

                    break;
                case '2':
                    registrationService.Register();
                    break;
                case '3':
                    return;
            }
        }
    }
}
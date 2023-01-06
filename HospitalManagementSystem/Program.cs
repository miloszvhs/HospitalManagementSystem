using System;
using HospitalManagementSystem.Application.Operations;
using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

internal class Program
{
    public static void Main()
    {
        IPasswordHasherService passwordService = new PasswordHasherService();
        IDatabaseService databaseService = new DatabaseService(passwordService);

        databaseService.RestoreFromXmlFile();
        
        IShiftService shiftService = new ShiftService(databaseService);
        IPWZNumberService pwzNumberService = new PWZNumberService(databaseService);
        IRegistrationService registrationService = new RegistrationService(databaseService, passwordService);
        ILoginService loginService = new LoginService(databaseService, passwordService);
        IMenuActionService menuService = new MenuActionService();

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
                                var adminOperations = new AdminOperations(menuService,
                                    databaseService,
                                    shiftService,
                                    passwordService,
                                    pwzNumberService,
                                    employee);

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
            }
        }
    }
}
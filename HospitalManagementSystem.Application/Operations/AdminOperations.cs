using System.Collections;
using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

namespace HospitalManagementSystem.Application.Operations;

public class AdminOperations
{
    private readonly MenuActionService _menuActionService;
    private readonly IDatabaseService<Admin> _adminDatabase;
    private readonly IDatabaseService<Doctor> _doctorDatabase;
    private readonly IDatabaseService<Employee> _employeeDatabase;


    public AdminOperations(MenuActionService menuActionService,
        IDatabaseService<Admin> adminDatabase,
        IDatabaseService<Doctor> doctorDatabase,
        IDatabaseService<Employee> employeeDatabase)
    {
        _menuActionService = menuActionService;
        _adminDatabase = adminDatabase;
        _doctorDatabase = doctorDatabase;
        _employeeDatabase = employeeDatabase;
    }

    public void Run()
    {
        while (true)
        {
            _menuActionService.DrawMenuViewByMenuType("Admin");

            var input = Console.ReadKey();
            
            switch (input.KeyChar)
            {
                case '1':

                    break;
                case '2':
                    foreach (var objectsList in new List<object>() { _adminDatabase.Users, _doctorDatabase.Users, _employeeDatabase.Users})
                    {
                        //NAPRAWIC, JAK WYDOBYC LISTE Z LISTY OBIEKTÓW
                        foreach (var (user, index) in usersList.Select((x, y) => (x, y + 1)))
                        {
                            Console.Write($"{index}.\tTyp\tImie");
                            if(ReferenceEquals(user, typeof(Admin)))
                            {
                                Console.WriteLine($"Admin\t{user}");
                            }
                            else if(ReferenceEquals(user, typeof(Doctor)))
                            {
                                Console.WriteLine($"Lekarz\t{user}");

                            } else if (ReferenceEquals(user, typeof(Employee)))
                            {
                                Console.WriteLine($"Pracownik\t{user}");

                            }
                        }
                    }
                    break;
                case '3':
                    break;
                case '4':
                    break;
                case '5':
                    break;
                default:
                    Console.WriteLine("Niepoprawny input");
                    break;
            }
        }
    }
}
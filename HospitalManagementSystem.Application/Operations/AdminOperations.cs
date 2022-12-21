using System.Collections;
using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Domain.ValueObjects;
using Microsoft.VisualBasic;

namespace HospitalManagementSystem.Application.Operations;

public class AdminOperations
{
    private readonly MenuActionService _menuActionService;
    private readonly IShiftService _shiftService;
    private readonly IDatabaseService<Admin> _adminDatabase;
    private readonly IDatabaseService<Doctor> _doctorDatabase;
    private readonly IDatabaseService<Employee> _employeeDatabase;


    public AdminOperations(MenuActionService menuActionService,
        IDatabaseService<Admin> adminDatabase,
        IDatabaseService<Doctor> doctorDatabase,
        IDatabaseService<Employee> employeeDatabase,
        IShiftService shiftService)
    {
        _menuActionService = menuActionService;
        _adminDatabase = adminDatabase;
        _doctorDatabase = doctorDatabase;
        _employeeDatabase = employeeDatabase;
        _shiftService = shiftService;
    }

    public void Run()
    {
        while (true)
        {
            _menuActionService.DrawMenuViewByMenuType("Admin");

            var input = Console.ReadKey();
            Console.WriteLine();
            
            switch (input.KeyChar)
            {
                case '1':
                    _shiftService.ShowShifts();
                    break;
                case '2':
                    ShowUsers();
                    break;
                case '3':
                    AddUser();
                    break;
                case '4':
                    DeleteUser();
                    break;
                case '5':
                    ChangeUser();
                    break;
                case '6':
                    return;
                default:
                    Console.WriteLine("Niepoprawny input");
                    break;
            }
        }
    }

    private void ChangeUser()
    {
        Console.Write("Podaj ID użytkownika, którego chcesz zmienić: ");
        var userId = Console.ReadLine();
    }

    private void DeleteUser()
    {
        Console.Write("Podaj ID użytkownika, które chcesz usunąć: ");
        var userId = Console.ReadLine();
    }

    private void AddUser()
    {
        throw new NotImplementedException();
    }

    private void ShowUsers()
    {
        Console.Write($"Numer\tId\tTyp\t\tImie\t\tPWZ\tSpecjalizacja\n");

        foreach (var objectsList in new List<IEnumerable<Employee>>() { _adminDatabase.Users, _doctorDatabase.Users, _employeeDatabase.Users})
        {
            foreach (var (user, index) in objectsList.Select((x, y) => (x, y + 1)))
            {
                switch (user.GetType().Name)
                {
                    case "Admin":
                        Console.WriteLine($"{index}.\t{user.Id}\t{user.GetType().Name}\t{user.Name.Value}");
                        break;
                    case "Doctor":
                        var doctorUser = (Doctor)user;
                        Console.WriteLine($"{index}.\t{user.Id}\t{user.GetType().Name}\t\t{String.Format("{0, -15}", user.Name.Value)}\t{doctorUser.Pwz.Value}\t{doctorUser.Specjalizacja}");
                        break;
                    case "Employee":
                        Console.WriteLine($"{index}.\t{user.Id}\t{user.GetType().Name}\t{user.Name.Value}");
                        break;
                }
            }
        } 
    }
}
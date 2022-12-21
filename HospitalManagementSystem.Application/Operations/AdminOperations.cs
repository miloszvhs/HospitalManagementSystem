using System.Collections;
using System.Diagnostics;
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
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IPWZNumberService _pwzNumberService;
    private readonly IDatabaseService<Admin> _adminDatabase;
    private readonly IDatabaseService<Doctor> _doctorDatabase;
    private readonly IDatabaseService<Employee> _employeeDatabase;


    public AdminOperations(MenuActionService menuActionService,
        IDatabaseService<Admin> adminDatabase,
        IDatabaseService<Doctor> doctorDatabase,
        IDatabaseService<Employee> employeeDatabase,
        IShiftService shiftService,
        IPasswordHasherService passwordHasherService, 
        IPWZNumberService pwzNumberService)
    {
        _menuActionService = menuActionService;
        _adminDatabase = adminDatabase;
        _doctorDatabase = doctorDatabase;
        _employeeDatabase = employeeDatabase;
        _shiftService = shiftService;
        _passwordHasherService = passwordHasherService;
        _pwzNumberService = pwzNumberService;
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
        Console.Write("Imię: ");
        var name = Console.ReadLine();
        Console.Write("Nazwisko: ");
        var lastName = Console.ReadLine();
        Console.Write("Nazwa użytkownika: ");
        var username = Console.ReadLine();
        Console.Write("Hasło: ");
        var password = _passwordHasherService.HashPassword(Console.ReadLine());
        Console.Write("Rola: ");
        var role = Console.ReadLine();

        try
        {
            switch (role)
            {
                case "0":
                    _employeeDatabase.AddUser(new Employee(new HospitalManagementSystemUsername(username),
                        new HospitalManagementSystemPassword(password),
                        new HospitalManagementSystemId(_employeeDatabase.GetLastId() + 1),
                        new HospitalManagementSystemName(name),
                        new HospitalManagementSystemName(lastName)));
                    _employeeDatabase.SaveToXmlFile();
                    break;
                case "1":
                    Console.WriteLine("Specjalizacja: ");
                    _menuActionService.DrawMenuViewByMenuType("Specialization");
                    var specialization = Console.ReadLine();

                    var userSpecialization = specialization switch
                    {
                        "1" => Specjalizacja.Kardiolog,
                        "2" => Specjalizacja.Urolog,
                        "3" => Specjalizacja.Laryngolog,
                        "4" => Specjalizacja.Neurolog,
                        _ => throw new Exception("Wybrano niepoprawną specjalizację")
                    };
                    
                    _doctorDatabase.AddUser(new Doctor(new HospitalManagementSystemUsername(username),
                        new HospitalManagementSystemPassword(password), 
                        _doctorDatabase.GetLastId() + 1,
                        new HospitalManagementSystemName(name), 
                        new HospitalManagementSystemName(lastName), 
                        new HospitalManagementSystemPWZ(_pwzNumberService.GetNewPWZ().ToString()), 
                        userSpecialization));
                    _doctorDatabase.SaveToXmlFile();
                    break;
                case "2":
                    _adminDatabase.AddUser(new Admin(new HospitalManagementSystemUsername(username), 
                        new HospitalManagementSystemPassword(password),
                        _adminDatabase.GetLastId() + 1, 
                        new HospitalManagementSystemName(name),
                        new HospitalManagementSystemName(lastName)));
                    _adminDatabase.SaveToXmlFile();
                    break;
            }

            Console.WriteLine($"Pomyślnie utworzono użytkownika {username}");
        }
        catch (Exception e)
        {
            Console.WriteLine("Coś poszło nie tak z dodawaniem użytkownika:");
            Console.WriteLine(e.Message);
        }
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
                        Console.WriteLine($"{index}.\t{user.Id}\t{String.Format("{0, -10}", user.GetType().Name)}\t{user.Name.Value}");
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
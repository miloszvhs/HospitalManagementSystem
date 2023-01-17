﻿using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Domain.ValueObjects;
using HospitalManagementSystem.Shared.Abstractions.Helpers;

namespace HospitalManagementSystem.Application.Operations;

public class AdminOperations
{
    private readonly IDatabaseService _database;
    private readonly Employee _employee;
    private readonly IMenuActionService _menuActionService;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IPWZNumberService _pwzNumberService;
    private readonly IShiftService _shiftService;

    public AdminOperations(IMenuActionService menuActionService,
        IDatabaseService database,
        IShiftService shiftService,
        IPasswordHasherService passwordHasherService,
        IPWZNumberService pwzNumberService,
        Employee employee)
    {
        _menuActionService = menuActionService;
        _database = database;
        _shiftService = shiftService;
        _passwordHasherService = passwordHasherService;
        _pwzNumberService = pwzNumberService;
        _employee = employee;
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
                    _shiftService.Run();
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
        var userId = Helper.CheckStringAndConvertToInt(Console.ReadLine());

        try
        {
            if(_database.GetEmployee(userId) == null)
            {
                throw new Exception("Użytkownik o takim ID nie istnieje.");
            }
            
            if (userId != _employee.Id)
            {
                Console.Write("Imię: ");
                var name = Console.ReadLine();

                Console.Write("Nazwisko: ");
                var lastName = Console.ReadLine();

                Console.Write("Nazwa użytkownika: ");
                var username = Console.ReadLine();

                Console.Write("Pesel: ");
                var pesel = Console.ReadLine();

                Console.Write("Hasło: ");
                var password = _passwordHasherService.HashPassword(Console.ReadLine());

                _menuActionService.DrawMenuViewByMenuType("Roles");
                Console.Write("Rola: ");
                var role = Console.ReadLine();
            
                var user = _database.GetEmployee(userId);

                if (user is not null)
                {
                    switch (role)
                    {
                        case "0":
                            var index = _database.Items.FindIndex(x => x.Id == user.Id);

                            var employee = new Employee(new HospitalManagementSystemUsername(username),
                                new HospitalManagementSystemPassword(password),
                                new HospitalManagementSystemPesel(pesel),
                                new HospitalManagementSystemId(userId),
                                new HospitalManagementSystemName(name),
                                new HospitalManagementSystemName(lastName),
                                Role.Pracownik);

                            if (_database.Items.Find(x => x.Pesel == employee.Pesel && x.Pesel != user.Pesel) != null)
                            {
                                throw new Exception("Użytkownik z takim peselem już istnieje.");
                            }

                            if (_database.Items.Find(
                                    x => x.Username == employee.Username && x.Username != user.Username) != null)
                            {
                                throw new Exception("Taki użytkownik już istnieje.");
                            }

                            _database.Items.RemoveAt(index);
                            _database.Items.Insert(index, employee);
                            _database.SaveToXmlFile();
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

                            index = _database.Items.FindIndex(x => x.Id == user.Id);

                            employee = new Employee(new HospitalManagementSystemUsername(username),
                                new HospitalManagementSystemPassword(password),
                                new HospitalManagementSystemPesel(pesel),
                                new HospitalManagementSystemId(userId),
                                new HospitalManagementSystemName(name),
                                new HospitalManagementSystemName(lastName),
                                Role.Lekarz,
                                new DoctorPrivileges(
                                    new HospitalManagementSystemPWZ(_pwzNumberService.GetNewPWZ().ToString()),
                                    userSpecialization));

                            if (_database.Items.Find(x => x.Pesel == employee.Pesel && x.Pesel != user.Pesel) != null)
                            {
                                throw new Exception("Użytkownik z takim peselem już istnieje.");
                            }

                            if (_database.Items.Find(
                                    x => x.Username == employee.Username && x.Username != user.Username) != null)
                            {
                                throw new Exception("Taki użytkownik już istnieje.");
                            }

                            _database.Items.RemoveAt(index);
                            _database.Items.Insert(index, employee);
                            _database.SaveToXmlFile();
                            break;
                        case "2":
                            index = _database.Items.FindIndex(x => x.Id == user.Id);

                            employee = new Employee(new HospitalManagementSystemUsername(username),
                                new HospitalManagementSystemPassword(password),
                                new HospitalManagementSystemPesel(pesel),
                                new HospitalManagementSystemId(userId),
                                new HospitalManagementSystemName(name),
                                new HospitalManagementSystemName(lastName),
                                Role.Administrator);

                            if (_database.Items.Find(x => x.Pesel == employee.Pesel && x.Pesel != user.Pesel) != null)
                            {
                                throw new Exception("Użytkownik z takim peselem już istnieje.");
                            }

                            if (_database.Items.Find(
                                    x => x.Username == employee.Username && x.Username != user.Username) != null)
                            {
                                throw new Exception("Taki użytkownik już istnieje.");
                            }

                            _database.Items.RemoveAt(index);
                            _database.Items.Insert(index, employee);
                            _database.SaveToXmlFile();
                            break;
                    }

                    Console.WriteLine($"Pomyślnie zmieniono użytkownika o id {userId}");
                }
                else
                {
                    Console.WriteLine("Dany użytkownik nie istnieje.");
                }
            }
            else
            {
                Console.WriteLine("Nie można zmienić samego siebie!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Coś poszło nie tak ze zmianą użytkownika:");
            Console.WriteLine(e.Message);
        }
    }

    private void DeleteUser()
    {
        Console.Write("Podaj ID użytkownika, które chcesz usunąć: ");
        var userId = Helper.CheckStringAndConvertToInt(Console.ReadLine());

        if (userId != _employee.Id)
        {
            if (_database.RemoveEmployee(userId) == -1)
            {
                Console.WriteLine("Użytkownik nie istnieje.");
            }
            else
            {
                Console.WriteLine($"Pomyślnie usunięto użytkownika o ID {userId}");
            }
        }
        else
        {
            Console.WriteLine("Nie można usunąć samego siebie!");
        }
    }

    private void AddUser()
    {
        Console.Write("Imię: ");
        var name = Console.ReadLine();

        Console.Write("Nazwisko: ");
        var lastName = Console.ReadLine();

        Console.Write("Nazwa użytkownika: ");
        var username = Console.ReadLine();

        Console.Write("Pesel: ");
        var pesel = Console.ReadLine();

        Console.Write("Hasło: ");
        var password = _passwordHasherService.HashPassword(Console.ReadLine());

        _menuActionService.DrawMenuViewByMenuType("Roles");
        Console.Write("Rola: ");
        var role = Console.ReadLine();

        try
        {
            switch (role)
            {
                case "0":
                    var employee = new Employee(new HospitalManagementSystemUsername(username),
                        new HospitalManagementSystemPassword(password),
                        new HospitalManagementSystemPesel(pesel),
                        new HospitalManagementSystemId(_database.GetLastId() + 1),
                        new HospitalManagementSystemName(name),
                        new HospitalManagementSystemName(lastName),
                        Role.Pracownik);

                    if (_database.Items.Find(x => x.Pesel == employee.Pesel) != null)
                    {
                        throw new Exception("Użytkownik z takim peselem już istnieje.");
                    }
                    
                    if(_database.Items.Find(x => x.Username == employee.Username) != null)
                    {
                        throw new Exception("Taki użytkownik już istnieje.");
                    }
                    
                    _database.AddEmployee(employee);
                    _database.SaveToXmlFile();
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

                    employee = new Employee(new HospitalManagementSystemUsername(username),
                        new HospitalManagementSystemPassword(password),
                        new HospitalManagementSystemPesel(pesel),
                        _database.GetLastId() + 1,
                        new HospitalManagementSystemName(name),
                        new HospitalManagementSystemName(lastName),
                        Role.Lekarz,
                        new DoctorPrivileges(new HospitalManagementSystemPWZ(_pwzNumberService.GetNewPWZ().ToString()),
                            userSpecialization)
                    );
                    
                    if (_database.Items.Find(x => x.Pesel == employee.Pesel) != null)
                    {
                        throw new Exception("Użytkownik z takim peselem już istnieje.");
                    }
                    
                    if(_database.Items.Find(x => x.Username == employee.Username) != null)
                    {
                        throw new Exception("Taki użytkownik już istnieje.");
                    }
                    
                    _database.AddEmployee(employee);
                    _database.SaveToXmlFile();
                    break;
                case "2":
                    employee = new Employee(new HospitalManagementSystemUsername(username),
                        new HospitalManagementSystemPassword(password),
                        new HospitalManagementSystemPesel(pesel),
                        _database.GetLastId() + 1,
                        new HospitalManagementSystemName(name),
                        new HospitalManagementSystemName(lastName),
                        Role.Administrator);
                    
                    if (_database.Items.Find(x => x.Pesel == employee.Pesel) != null)
                    {
                        throw new Exception("Użytkownik z takim peselem już istnieje.");
                    }
                    
                    if(_database.Items.Find(x => x.Username == employee.Username) != null)
                    {
                        throw new Exception("Taki użytkownik już istnieje.");
                    }
                    
                    _database.AddEmployee(employee);
                    _database.SaveToXmlFile();
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
        Console.Write("Numer\tId\tTyp\t\tImie\t\tPWZ\tSpecjalizacja\n");

        foreach (var (user, index) in _database.Items.Select((x, y) => (x, y + 1)))
        {
            switch (user.Rola)
            {
                case Role.Administrator:
                    Console.WriteLine(
                        $"{index}.\t{user.Id}\t{string.Format("{0, -10}", user.Rola)}\t{string.Format("{0, -10}", user.Name.Value)}\t-\t-");
                    break;
                case Role.Lekarz:
                    Console.WriteLine(
                        $"{index}.\t{user.Id}\t{user.Rola}\t\t{string.Format("{0, -15}", user.Name.Value)}\t{user.DoctorPrivileges.Pwz.Value}\t{user.DoctorPrivileges.Specjalizacja}");
                    break;
                case Role.Pracownik:
                    Console.WriteLine(
                        $"{index}.\t{user.Id}\t{user.Rola}\t{string.Format("{0, -10}", user.Name.Value)}\t-\t-");
                    break;
            } 
        }
    }
}
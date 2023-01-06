using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Application.Operations;

public class AdminOperations
{
    private readonly IDatabaseService _database;
    private readonly IMenuActionService _menuActionService;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IPWZNumberService _pwzNumberService;
    private readonly IShiftService _shiftService;
    private readonly Employee _employee;

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
            _shiftService.SetEmployee(_employee);
            _menuActionService.DrawMenuViewByMenuType("Admin");

            var input = Console.ReadKey();
            Console.WriteLine();

            switch (input.KeyChar)
            {
                case '1':
                    _shiftService.ShowAllShifts();
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
        
        _menuActionService.DrawMenuViewByMenuType("Roles");
        Console.Write("Rola: ");
        var role = Console.ReadLine();

        try
        {
            switch (role)
            {
                case "0":
                    _database.AddEmployee(new Employee(new HospitalManagementSystemUsername(username),
                        new HospitalManagementSystemPassword(password),
                        new HospitalManagementSystemId(_database.GetLastId() + 1),
                        new HospitalManagementSystemName(name),
                        new HospitalManagementSystemName(lastName),
                        Role.Pracownik));
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

                    _database.AddEmployee(new Employee(new HospitalManagementSystemUsername(username),
                        new HospitalManagementSystemPassword(password),
                        _database.GetLastId() + 1,
                        new HospitalManagementSystemName(name),
                        new HospitalManagementSystemName(lastName),
                        Role.Lekarz,
                        new DoctorPrivileges(new HospitalManagementSystemPWZ(_pwzNumberService.GetNewPWZ().ToString()),
                        userSpecialization)
                        ));
                    _database.SaveToXmlFile();
                    break;
                case "2":
                    _database.AddEmployee(new Employee(new HospitalManagementSystemUsername(username),
                        new HospitalManagementSystemPassword(password),
                        _database.GetLastId() + 1,
                        new HospitalManagementSystemName(name),
                        new HospitalManagementSystemName(lastName),
                        Role.Administrator));
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

        foreach (var (user, index) in _database.Users.Select((x, y) => (x, y + 1)))
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
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

namespace HospitalManagementSystem.Application.Services;

public class ShiftService : IShiftService
{
    private readonly IShiftDatabaseService _shiftDatabase;
    private readonly IDatabaseService _database;
    private readonly IMenuActionService _menuActionService;

    public ShiftService(IShiftDatabaseService shiftDatabase,
        IMenuActionService menuActionService,
        IDatabaseService database)
    {
        _shiftDatabase = shiftDatabase;
        _menuActionService = menuActionService;
        _database = database;
    }

    private Employee actuallEmployee { get; set; }

    public void ShowDoctorShifts()
    {
        foreach (var shift in _shiftDatabase.Items.Where(x => x.Date >= DateTime.Now))
        {
            Console.WriteLine("Id\tNazwa uzytkownika\tImie\tNazwisko\tSpecjalizacja");
            foreach (var doctor in shift.Users.Where(x => x.Rola == Role.Lekarz))
                Console.WriteLine($"{doctor.Id}.\t{doctor.Username.Value}\t{doctor.Name.Value}\t{doctor.LastName.Value}\t{doctor.DoctorPrivileges.Specjalizacja}");
        }
    }

    public void ShowEmployeeShifts()
    {
        foreach (var shift in _shiftDatabase.Items.Where(x => x.Date >= DateTime.Now))
        {
            Console.WriteLine($"{shift.Date}");
            Console.WriteLine("Id\tNazwa uzytkownika\tImie\tNazwisko");
            foreach (var doctor in shift.Users.Where(x => x.Rola == Role.Pracownik))
                Console.WriteLine($"{doctor.Id}.\t{doctor.Username.Value}\t{doctor.Name.Value}\t{doctor.LastName.Value}");
        }
    }

    public void ShowAllShifts()
    {
        if (_shiftDatabase.Items.Any())
            foreach (var shift in _shiftDatabase.Items.Where(x => x.Date >= DateTime.Now))
            {
                Console.WriteLine($"{shift.Date}");
                Console.WriteLine("Id\tNazwa uzytkownika\tImie\tNazwisko\tSpecjalizacja");
                foreach (var employee in shift.Users)
                    Console.WriteLine(
                        $"{employee.Id}.\t{employee.Username.Value}\t\t\t{employee.Name.Value}\t{employee.LastName.Value}\t{employee.DoctorPrivileges?.Specjalizacja}");
            }
    }
    
    public void Run()
    {
        while (true)
        {
            switch (actuallEmployee.Rola)
            {
                case Role.Administrator:
                    _menuActionService.DrawMenuViewByMenuType("ShiftsForAdministrator");
                    
                    var input = Console.ReadKey();
                    Console.WriteLine();

                    switch (input.KeyChar)
                    {
                        case '1':
                            ShowAllShifts();
                            break;
                        case '2':
                            DateTime date;
                            
                            Console.Write("Podaj ID użytkownika, którego chcesz dodać: ");
                            try
                            {
                                var id = int.Parse(Console.ReadLine());
                                var user = _database.GetEmployee(id);
                            
                                Console.Write("Podaj date dyżuru w formacie DD/MM/YYYY lub DD-MM-YYYY:");
                                date = ParseDate(Console.ReadLine());

                                AddShift(date, user);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case '3':
                            Console.Write("Podaj date w formacie DD/MM/YYYY lub DD-MM-YYYY:");
                            date = ParseDate(Console.ReadLine());

                            RemoveShift(date, actuallEmployee);
                            break;
                        case '4':
                            return;
                    }
                    break;
                default:
                    _menuActionService.DrawMenuViewByMenuType("ShiftsForOthers");
                    input = Console.ReadKey();
                    Console.WriteLine();

                    switch (input.KeyChar)
                    {
                        case '1':
                            ShowAllShifts();
                            break;
                        case '2':
                            return;
                    }
                    break;
            }
        }
    }

    public int RemoveShift(DateTime date, Employee employee)
    {
        var shift = _shiftDatabase.Items.Find(x => x.Date.Date == date.Date && x.Users.Contains(employee));

        if (shift is not null)
        {
            shift.Users.Remove(employee);

            if (!shift.Users.Any())
            {
                _shiftDatabase.RemoveShift(shift.Id);
                _shiftDatabase.SaveToXmlFile();
            }

            return employee.Id;
        }

        return -1;
    }

    public int EditShift()
    {
        throw new NotImplementedException();
    }

    public void SetEmployee(Employee employee)
    {
        actuallEmployee = employee;
    }

    public int AddShift(DateTime date, Employee employee)
    {
        var shift = _shiftDatabase.Items.Find(x => x.Date.Date == date.Date);
        date = date.Date;

        try
        {
            if (shift is not null)
            {
                if (!CheckIfEmployeeHasLessThanTenShiftsInMonth(actuallEmployee))
                {
                    throw new Exception(
                        "Nie można dodać dyżuru, gdyż użytkownik ma już aktualnie zajętych 10 dyżurów w tym miesiącu.");
                }

                if(CheckIfEmployeeHasShiftOneDayBehind(date, employee) &&
                 CheckIfEmployeeHasShiftOneDayForward(date, employee))
                {
                    throw new Exception("Nie można dodać dyżuru, gdyż dany użytkownik posiada już dyżur dzień przed lub dzień po.");
                }
                    
                var user = shift.Users.FirstOrDefault(x => x.Id == employee.Id);

                if (user is not null)
                {
                    throw new Exception("Nie można dodać dyżuru, gdyż dyżur w danym dniu już istnieje.");
                }

                if (employee.Rola == Role.Lekarz)
                {
                    var doctorWithSameSpecialization = shift.Users.Where(x => x.DoctorPrivileges is not null)
                        .ToList()
                        .Find(x => x.DoctorPrivileges.Specjalizacja == employee.DoctorPrivileges.Specjalizacja);
                    if (doctorWithSameSpecialization is not null)
                        throw new Exception(
                            "Nie można dodać dyżuru, gdyż tego dnia dyżur ma już inny lekarz z tą samą specjalizacją.");
                }

                shift.Users.Add(employee);
                return employee.Id;
            }
            else
            {
                if (!CheckIfEmployeeHasLessThanTenShiftsInMonth(actuallEmployee))
                {
                    throw new Exception(
                        "Nie można dodać dyżuru, gdyż użytkownik ma już aktualnie zajętych 10 dyżurów w tym miesiącu.");
                }

                if (!CheckIfDateIsBeforeActuallDate(date))
                {
                    throw new Exception("Nie można dodać dyżuru, gdyż wybrana data jest wcześniej niż aktualna.");
                }
                
                if (CheckIfEmployeeHasShiftOneDayBehind(date, employee) ||
                    CheckIfEmployeeHasShiftOneDayForward(date, employee))
                {
                    throw new Exception("Nie można dodać dyżuru, gdyż dany użytkownik posiada już dyżur dzień przed lub dzień po.");

                }
                
                var newShift = new Shift(date) { Users = { actuallEmployee } };
                _shiftDatabase.AddShift(newShift);
                _shiftDatabase.SaveToXmlFile();
                
                Console.WriteLine("Dodano dyżur.");
                return employee.Id;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return -1;
    }

    private bool CheckIfEmployeeHasShiftOneDayForward(DateTime date, Employee employee)
    {
        var shiftExist = _shiftDatabase.Items.Exists(x => x.Date.Date == date.AddDays(1).Date && x.Users.Contains(employee));
        return shiftExist;
    }

    private bool CheckIfEmployeeHasShiftOneDayBehind(DateTime date, Employee employee)
    {
        var shiftExist =
            _shiftDatabase.Items.Exists(x => x.Date.Date == date.AddDays(-1).Date && x.Users.Contains(employee));
        return shiftExist;
    }

    private bool CheckIfEmployeeHasLessThanTenShiftsInMonth(Employee employee)
    {
        var count = _shiftDatabase.Items
            .Where(x => x.Date.Month == DateTime.Now.Month)
            .Count(x => x.Users.Contains(employee));

        if (count < 10) return true;

        return false;
    }

    private bool CheckIfDateIsBeforeActuallDate(DateTime date)
    {
        if (date.Date >= DateTime.Now.Date) return true;

        return false;
    }

    public DateTime ParseDate(string text)
    {
        DateTime date;

        try
        {
            if (!DateTime.TryParse(text, out date)) throw new Exception($"Cannot parse {text} to date.");

            return date;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return default;
    }
}
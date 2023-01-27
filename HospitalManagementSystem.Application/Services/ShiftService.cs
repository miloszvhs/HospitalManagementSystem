using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Shared.Abstractions.Helpers;

namespace HospitalManagementSystem.Application.Services;

public class ShiftService : IShiftService
{
    private readonly IShiftDatabaseService _shiftDatabase;
    private readonly IDatabaseService _database;
    private readonly IMenuActionService _menuActionService;
    private Employee actuallEmployee { get; set; }

    public ShiftService(IShiftDatabaseService shiftDatabase,
        IMenuActionService menuActionService,
        IDatabaseService database)
    {
        _shiftDatabase = shiftDatabase;
        _menuActionService = menuActionService;
        _database = database;
    }
    
    public void Run()
    {
        while (true)
        {
            switch (actuallEmployee.Role)
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
                                var id = Helper.CheckStringAndConvertToInt(Console.ReadLine());
                                var user = _database.GetEmployee(id);

                                if(CheckIfEmployeeIsNull(user))
                                {
                                    throw new Exception("Użytkownik nie istnieje.");
                                }
                                
                                Console.Write("Podaj date dyżuru w formacie DD/MM/YYYY lub DD-MM-YYYY:");
                                date = Helper.ParseDate(Console.ReadLine());

                                if (AddShift(date, user) != -1)
                                {
                                    Console.WriteLine("Dodano dyżur.");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case '3':
                            Console.Write("Podaj date w formacie DD/MM/YYYY lub DD-MM-YYYY:");
                            date = Helper.ParseDate(Console.ReadLine());

                            RemoveShift(date, actuallEmployee);
                            break;
                        case '4':
                            break;
                        case '5':
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

    public void ShowAllShifts()
    {
        if (_shiftDatabase.Items.Any())
        {
            _menuActionService.DrawShifts(_shiftDatabase.Items);
        }
        else
        {
            Console.WriteLine("Nie ma żadnych dyżurów.");
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
                if (!CheckIfEmployeeHasLessThanTenShiftsInMonth(employee, date))
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

                if (employee.Role == Role.Lekarz)
                {
                    var doctorWithSameSpecialization = shift.Users.Where(x => x.DoctorPrivileges is not null)
                        .ToList()
                        .Find(x => x.DoctorPrivileges.DoctorSpecialization == employee.DoctorPrivileges.DoctorSpecialization);
                    if (doctorWithSameSpecialization is not null)
                        throw new Exception(
                            "Nie można dodać dyżuru, gdyż tego dnia dyżur ma już inny lekarz z tą samą specjalizacją.");
                }

                shift.Users.Add(employee);
                _shiftDatabase.SaveToXmlFile();
                
                return employee.Id;
            }
            else
            {
                if (!CheckIfEmployeeHasLessThanTenShiftsInMonth(employee, date))
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
                
                var newShift = new Shift(date) { Users = { employee } };
                _shiftDatabase.AddShift(newShift);
                _shiftDatabase.SaveToXmlFile();
                
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

    private bool CheckIfEmployeeHasLessThanTenShiftsInMonth(Employee employee, DateTime month)
    {
        var count = _shiftDatabase.Items
            .Where(x => x.Date.Month == month.Month)
            .Count(x => x.Users.Contains(employee));

        if (count < 10) return true;

        return false;
    }

    private bool CheckIfDateIsBeforeActuallDate(DateTime date)
    {
        if (date.Date >= DateTime.Now.Date) return true;

        return false;
    }

    private bool CheckIfEmployeeIsNull(Employee employee)
    {
        if (employee is null)
        {
            return true;
        }

        return false;
    }
}
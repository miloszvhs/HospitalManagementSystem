using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Exceptions;
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
                            Employee user;

                            Console.Write("Podaj ID użytkownika, którego chcesz dodać: ");
                            var id = Helper.CheckStringAndConvertToInt(Console.ReadLine());
                            user = _database.GetEmployee(id);
                            
                            try
                            {
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
                            (date, user) = CheckAndGetDateAndEmployee();
                            
                            RemoveShift(date, user);
                            break;
                        case '4':
                            (date, user) = CheckAndGetDateAndEmployee();

                            EditShift(date, user);
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
        var shift = _shiftDatabase.Items.Find(x => x.Date.Date == date.Date && x.Users.Exists(y => y.Id == employee.Id));

        if (shift is not null)
        {
            shift.RemoveEmployee(employee.Id);

            Console.WriteLine($"Usunięto dyżur dnia {date.ToShortDateString()} pracownika o numerze id:{employee.Id}");     
            
            if (!shift.Users.Any())
            {
                _shiftDatabase.RemoveShift(shift.Id);
            }
            _shiftDatabase.SaveToXmlFile();
            
            return employee.Id;
        }

        return -1;
    }

    public int EditShift(DateTime date, Employee employee)
    {
        var shift = _shiftDatabase.Items.Find(x => x.Date.Date == date);
        
        if(shift is null)
        {
            throw new ShiftDoesntExistsException();
        }

        var user = shift.Users.Select(x => x.Id == employee.Id);
        
        if(user is null)
        {
            throw new UserWithIdDoesntExistException(employee.Id);
        }
        
        var result = EditMenu(date, employee);
        return result;
    }
    
    public void SetEmployee(Employee employee)
    {
        actuallEmployee = employee;
    }

    public int AddShift(DateTime date, Employee employee)
    {
        var shift = _shiftDatabase.Items.Find(x => x.Date.Date == date.Date);
    
        try
        {
            if (!CheckIfEmployeeHasLessThanTenShiftsInMonth(employee, date))
            {
                throw new UserHasAlreadyMaximalNumberOfShiftsException();
            }
            
            if (!CheckIfDateIsBeforeCurrentDate(date))
            {
                throw new GivenDateIsBeforeCurrentDateException();
            }
            
            if(CheckIfEmployeeHasShiftOneDayBehind(date, employee) ||
               CheckIfEmployeeHasShiftOneDayForward(date, employee))
            {
                throw new UserHasAlreadyAShiftDayBeforeOrDayAfterException();
            }
            
            if (shift is not null)
            {
                shift.AddEmployee(employee);
                _shiftDatabase.SaveToXmlFile();
            }
            else
            {
                var newShift = new Shift(date) { Users = { employee } };
                _shiftDatabase.AddShift(newShift);
                _shiftDatabase.SaveToXmlFile();
            }
            
            return employee.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return -1;
        }
    }
    
    private int EditMenu(DateTime date, Employee employee)
    {
        while (true)
        {
            _menuActionService.DrawMenuViewByMenuType("ShiftEdit");
            var input = Console.ReadKey();
            
            switch (input.KeyChar)
            {
                case '1':
                    //
                    break;
                case '2':
                    return RemoveShift(date, employee);
                case '3':
                    return default;
            }            
        }
    }

    private (DateTime, Employee) CheckAndGetDateAndEmployee()
    {
        Console.Write("Podaj ID użytkownika, którego chcesz wybrać: ");
        var id = Helper.CheckStringAndConvertToInt(Console.ReadLine());
        
        var user = _database.GetEmployee(id);
        
        Console.Write("Podaj date w formacie DD/MM/YYYY lub DD-MM-YYYY:");
        var date = Helper.ParseDate(Console.ReadLine());
        
        return (date, user);
    }
    
    private bool CheckIfEmployeeHasShiftOneDayForward(DateTime date, Employee employee)
    {
        var shiftExist = _shiftDatabase.Items.Exists(x => x.Date.Date == date.AddDays(1).Date && x.Users.Exists(y => y.Id == employee.Id));
        return shiftExist;
    }

    private bool CheckIfEmployeeHasShiftOneDayBehind(DateTime date, Employee employee)
    {
        var shiftExist =
            _shiftDatabase.Items.Exists(x => x.Date.Date == date.AddDays(-1).Date && x.Users.Exists(y => y.Id == employee.Id));
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

    private bool CheckIfDateIsBeforeCurrentDate(DateTime date)
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
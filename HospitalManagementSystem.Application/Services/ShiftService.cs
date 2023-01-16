using AutoMapper;
using HospitalManagementSystem.Application.DTOModels;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Application.Services;

public class ShiftService : IShiftService
{
    private Employee actuallEmployee { get; set; }
    private readonly IShiftDatabaseService _database;
    private readonly IMenuActionService _menuActionService;

    public ShiftService(IShiftDatabaseService database, 
        IMenuActionService menuActionService)
    {
        _database = database;
        _menuActionService = menuActionService;
    }
    
    public void ShowDoctorShifts()
    {
        foreach (var shift in _database.Items.Where(x => x.Date >= DateTime.Now))
        {
            foreach (var doctor in shift.Users.Where(x => x.Rola == Role.Lekarz))
            {
                if (doctor.Username == actuallEmployee.Username)
                {
                    Console.WriteLine($"{doctor.Id}. {doctor.Username.Value} {doctor.DoctorPrivileges.Specjalizacja}");
                }
            }
        }
    }

    public void ShowEmployeeShifts()
    {
        foreach (var shift in _database.Items.Where(x => x.Date >= DateTime.Now))
        {
            Console.WriteLine($"{shift.Date}");
            foreach (var doctor in shift.Users.Where(x => x.Rola == Role.Pracownik))
            {
                Console.WriteLine($"{doctor.Id}. {doctor.Username.Value}");
            } 
        }
    }
    
    public void ShowAllShifts()
    {
        if (_database.Items.Any())
        {
            foreach (var shift in _database.Items.Where(x => x.Date >= DateTime.Now))
            {
                Console.WriteLine($"{shift.Date}");
                foreach (var employee in shift.Users)
                {
                    Console.WriteLine($"{employee.Id}. {employee.Username.Value} {employee.DoctorPrivileges?.Specjalizacja}");
                }
            }    
        }
    }

    public void Run()
    {
        while (true)
        {   
            _menuActionService.DrawMenuViewByMenuType("Shifts");
            
            var input = Console.ReadKey();
            Console.WriteLine();

            switch (input.KeyChar)
            {
                case '1':
                    switch (actuallEmployee.Rola)
                    {
                        case Role.Administrator:
                            ShowAllShifts();
                            break;
                        case Role.Lekarz:
                            ShowDoctorShifts();
                            break;
                        case Role.Pracownik:
                            ShowEmployeeShifts();
                            break;
                    }
                    break;
                case '2':
                    Console.Write("Podaj date w formacie DD/MM/YYYY lub DD-MM-YYYY:");
                    var date = ParseDate(Console.ReadLine());

                    AddShift(date, actuallEmployee);
                    break;
                case '3':
                    Console.Write("Podaj date w formacie DD/MM/YYYY lub DD-MM-YYYY:");
                    date = ParseDate(Console.ReadLine());

                    RemoveShift(date, actuallEmployee);
                    break;
                case '4':
                    return;
                default:
                    break;
            }
            
        }
    }

    public int RemoveShift(DateTime date, Employee employee)
    {
        var shift = _database.Items.Find(x => x.Date.Date == date.Date && x.Users.Contains(employee));
        
        if(shift is not null)
        {
            shift.Users.Remove(employee);
            
            if(!shift.Users.Any())
            {
                _database.Items.Remove(shift);
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
        var shift = _database.Items.Find(x => x.Date.Date == date.Date);
        date = date.Date;

        try
        {
            if (shift is not null)
            {
                if (!CheckIfEmployeeHasLessThanTenShiftsInMonth(actuallEmployee))
                {
                    throw new Exception("Nie można dodać dyżuru, gdyż użytkownik ma już aktualnie zajętych 10 dyżurów w tym miesiącu.");
                }
                
                var user = shift.Users.FirstOrDefault(x => x.Id == employee.Id);

                if (user is not null)
                {
                    throw new Exception("Nie można dodać dyżuru, gdyż dyżur w danym dniu już istnieje.");
                }
                else
                {
                    if(employee.Rola == Role.Lekarz)
                    {
                        var doctorWithSameSpecialization = shift.Users.Find(x => x.DoctorPrivileges.Specjalizacja == employee.DoctorPrivileges.Specjalizacja);
                        if (doctorWithSameSpecialization is not null)
                        {
                            throw new Exception(
                                "Nie można dodać dyżuru, gdyż tego dnia dyżur ma już inny lekarz z tą samą specjalizacją.");
                        }
                    }
                    shift.Users.Add(employee);
                    return employee.Id;
                }
            }
            else
            {
                if (!(CheckIfEmployeeHasShiftOneDayBehind(date, employee)) &&
                    !(CheckIfEmployeeHasShiftOneDayForward(date, employee)))
                {
                    if(!CheckIfDateIsBeforeActuallDate(date))
                    {
                        throw new Exception("Nie można dodać dyżuru, gdyż wybrana data jest wcześniej niż aktualna.");
                    }
                    
                    var newShift = new Shift(date) { Users = { actuallEmployee } };
                    _database.AddShift(newShift);; 
                    return employee.Id;
                }
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
        var shiftExist = _database.Items.Exists(x => x.Date.Date == date.AddDays(1).Date && x.Users.Contains(employee));
        return shiftExist;
    }

    private bool CheckIfEmployeeHasShiftOneDayBehind(DateTime date, Employee employee)
    {
        var shiftExist = _database.Items.Exists(x => x.Date.Date == date.AddDays(-1).Date && x.Users.Contains(employee));
        return shiftExist;
    }

    private bool CheckIfEmployeeHasLessThanTenShiftsInMonth(Employee employee)
    {
        var count = _database.Items
            .Where(x => x.Date.Month == DateTime.Now.Month)
            .Count(x => x.Users.Contains(employee));

        if (count <= 10)
        {
            return true;
        }

        return false;
    }

    private bool CheckIfDateIsBeforeActuallDate(DateTime date)
    {
        if(date.Date >= DateTime.Now.Date)
        {
            return true;
        }

        return false;
    }

    public DateTime ParseDate(string text)
    {
        DateTime date;

        try
        {
            if (!DateTime.TryParse(text, out date))
            {
                throw new Exception($"Cannot parse {text} to date.");
            }

            return date;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return default;
    }
}
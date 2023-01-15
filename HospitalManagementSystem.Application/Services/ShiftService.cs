using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

namespace HospitalManagementSystem.Application.Services;

public class ShiftService : IShiftService
{
    private readonly IDatabaseService _database;
    private List<Shift> _shifts { get; } = new();
    private Employee actuallEmployee;
    private IMenuActionService _menuActionService;

    public ShiftService(IDatabaseService database, 
        IMenuActionService menuActionService)
    {
        _database = database;
        _menuActionService = menuActionService;
    }
    
    public void ShowDoctorShifts()
    {
        foreach (var doctor in _database.Users.Where(x => x.Rola == Role.Lekarz))
        {
            if(doctor.Username == actuallEmployee.Username)
            {
                
            }        
        }
    }

    public void ShowEmployeeShifts()
    {
        foreach (var doctor in _database.Users.Where(x => x.Rola == Role.Pracownik))
        {
                    
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
                    Console.Write("Podaj date w formacie DD/MM/YYYY:");
                    var date = ParseDate(Console.ReadLine());

                    AddShift(date, actuallEmployee);
                    break;
                case '3':
                    break;
                case '4':
                    return;
                default:
                    break;
            }
            
        }
    }

    public void ShowAllShifts()
    {
        if (_shifts.Any())
        {
            foreach (var shift in _shifts.Where(x => x.Date >= DateTime.Now))
            {
                Console.WriteLine($"{shift.Date}");
                foreach (var employee in shift.Users)
                {
                    Console.WriteLine($"{employee.Id}. {employee.Username} {employee.DoctorPrivileges?.Specjalizacja}");
                }
            }    
        }
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
        var shift = _shifts.Find(x => x.Date.Date == date.Date);
        date = date.Date;
        
        if (shift is not null)
        {
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
            }
        }
        else
        {
            if (!(CheckIfEmployeeHasShiftOneDayBehind(date, employee)) &&
                !(CheckIfEmployeeHasShiftOneDayForward(date, employee)))
            {
                _shifts.Add(new Shift(date));
            }
        }

        return employee.Id;
    }

    private bool CheckIfEmployeeHasShiftOneDayForward(DateTime date, Employee employee)
    {
        var shiftExist = _shifts.Exists(x => x.Date.Date == date.AddDays(1) && x.Users.Contains(employee));
        return shiftExist;
    }

    private bool CheckIfEmployeeHasShiftOneDayBehind(DateTime date, Employee employee)
    {
        var shiftExist = _shifts.Exists(x => x.Date.Date == date.AddDays(-1) && x.Users.Contains(employee));
        return shiftExist;
    }

    private bool CheckIfEmployeeHasLessThanTenShiftsInMonth(Employee employee)
    {
        var count = _shifts
            .Where(x => x.Date.Month == DateTime.Now.Month)
            .Count(x => x.Users.Contains(employee));

        if (count <= 10)
        {
            return true;
        }
        else
        {
            return false;
        }
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
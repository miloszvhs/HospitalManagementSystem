using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

namespace HospitalManagementSystem.Application.Services;

public class ShiftService : IShiftService
{
    private readonly IDatabaseService _database;
    private List<Shift> _shifts { get; } = new();
    private Employee actuallEmployee;

    public ShiftService(IDatabaseService database)
    {
        _database = database;
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

    public void ShowAllShifts()
    {
        foreach (var employee in _database.Users.Where(x => x.Rola == Role.Lekarz ||
                                                            x.Rola == Role.Pracownik))
        {
            
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
        var shift = _shifts.Find(x => x.Date == date);
        
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
            _shifts.Add(new Shift(date));
        }

        return employee.Id;
    }
    
    private DateTime? GetThePreviousShiftDate(DateTime date, Employee employee)
    {
        var shift = _shifts.FindLast(x => x.Users.Contains(employee));
        
        return shift?.Date;
    }
}
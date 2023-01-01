using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

namespace HospitalManagementSystem.Application.Services;

public class ShiftService : IShiftService
{
    private readonly List<Shift> Shifts = new();
    private readonly IDatabaseService _database;

    public ShiftService(IDatabaseService database)
    {
        _database = database;
    }
    
    public void ShowDoctorShifts()
    {
        foreach (var doctor in _database.GetAllEmployees().Where(x => x.Rola == Role.Lekarz))
        {
            
        }
    }

    public void ShowEmployeeShifts()
    {
        
    }

    public void ShowShifts()
    {
        ShowDoctorShifts();
        ShowEmployeeShifts();
    }

    public int AddShift(int id)
    {
        var user = "";
        return 1;
    }
    
    public int ChangeShift()
    {
        throw new NotImplementedException();
    }

    private bool CheckIfShiftIsPossible()
    {
        if (Shifts.)
        {
            
        }
        return true;
    }
    
    private List<Shift> RestoreShiftsFromXML()
    {
        return new List<Shift>();
    }

}
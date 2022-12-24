using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

namespace HospitalManagementSystem.Application.Services;

public class ShiftService : IShiftService
{
    private readonly List<Shift> Shifts;

    public ShiftService()
    {
        
    }
    
    public void ShowShifts()
    {
    }

    public int AddShift()
    {
        throw new NotImplementedException();
    }

    public int EditShift()
    {
        throw new NotImplementedException();
    }

    private bool CheckIfShiftIsPossible()
    {
        return true;
    }
    
    private List<Shift> RestoreShiftsFromXML()
    {
        return new List<Shift>();
    }

}
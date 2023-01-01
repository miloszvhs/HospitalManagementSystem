namespace HospitalManagementSystem.Domain.Interfaces;

public interface IShiftService
{
    public void ShowDoctorShifts();
    public void ShowEmployeeShifts();
    public void ShowShifts();
    public int AddShift(int id);
    public int ChangeShift();
}
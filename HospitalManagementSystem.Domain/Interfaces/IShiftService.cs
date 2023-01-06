using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Domain.Interfaces;

public interface IShiftService
{
    void ShowAllShifts();
    int AddShift(DateTime date, Employee employee);
    int EditShift();
    void SetEmployee(Employee employee);
}
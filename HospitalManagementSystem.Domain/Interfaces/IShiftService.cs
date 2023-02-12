using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Domain.Interfaces;

public interface IShiftService
{
    void ShowAllShifts();
    int AddShift(DateTime date, Employee employee);
    int RemoveShift(DateTime date, Employee employee);
    int EditShift(DateTime date, Employee employee);
    void SetEmployee(Employee employee);
    void Run();
}
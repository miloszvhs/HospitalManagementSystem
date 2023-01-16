using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Domain.Interfaces;

public interface IShiftService
{
    void ShowAllShifts();
    int AddShift(DateTime date, Employee employee);
    int RemoveShift(DateTime date, Employee employee);
    int EditShift();
    void SetEmployee(Employee employee);
    void ShowDoctorShifts();
    void ShowEmployeeShifts();
    void Run();
}
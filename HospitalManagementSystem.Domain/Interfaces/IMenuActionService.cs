using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Domain.Interfaces;

public interface IMenuActionService
{
    void DrawMenuViewByMenuType(string menuType);
    void DrawUsers(List<Employee> users);
    void DrawShifts(List<Shift> shifts);
}
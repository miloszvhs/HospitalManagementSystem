using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Domain.Interfaces;

public interface IDatabaseService
{
    void RestoreFromXmlFile();
    void SaveToXmlFile();
    void AddEmployee(Employee employee);
    int UpdateEmployee(Employee employee);
    int RemoveEmployee(int id);
    public List<Employee> GetAllEmployees();
    Employee GetEmployee(int id);
    int GetLastId();
}
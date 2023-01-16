using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Domain.Interfaces;

public interface IDatabaseService
{
    List<Employee> Items { get; set; }
    void RestoreFromXmlFile();
    void SaveToXmlFile();
    void AddEmployee(Employee employee);
    int UpdateEmployee(Employee employee);
    int RemoveEmployee(int id);
    Employee GetEmployee(int id);
    int GetLastId();
    void Seed();
}
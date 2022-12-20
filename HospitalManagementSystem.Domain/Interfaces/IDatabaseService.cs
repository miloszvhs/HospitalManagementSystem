using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Domain.Interfaces;

public interface IDatabaseService<T> where T : class
{
    List<T> Users { get; set; }
    void RestoreFromXmlFile();
    void SaveToXmlFile();
    void AddUser(T user);
    void UpdateUser(T user);
    void RemoveUser(T user);
    T GetUser(int id);
    int GetLastId();
}
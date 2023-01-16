using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Domain.Interfaces;

public interface IShiftDatabaseService
{
    List<Shift> Items { get; set; }
    void RestoreFromXmlFile();
    void SaveToXmlFile();
    void AddShift(Shift shift);
    int UpdateShift(Shift shift);
    int RemoveShift(int id);
    Shift GetShift(int id);
    int GetLastId();
}
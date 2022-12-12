using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Interfaces;

public interface IDatabaseService<T> where T : BaseEntity
{
    public void AddUser(T user);
    public void UpdateUser(T user);
    public void RemoveUser(T user);
    public T GetUser(int id);
}
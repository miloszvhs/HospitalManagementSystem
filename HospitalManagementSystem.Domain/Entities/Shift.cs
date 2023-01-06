using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Entities;

public class Shift : BaseEntity
{
    public DateTime Date { get; }
    public List<Employee> Users { get; } = new();

    public Shift(DateTime date)
    {
        Date = date;
    }
}
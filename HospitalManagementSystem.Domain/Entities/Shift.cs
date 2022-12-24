using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Entities;

public class Shift : BaseEntity
{
    public DateOnly Date { get; }
    public List<Employee> Users { get; } = new();
}
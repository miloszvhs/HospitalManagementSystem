using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Application.DTOModels;

public class ShiftDTO
{
    public DateOnly Date { get; set; }
    public List<EmployeeDTO> Users { get; set; }

    public ShiftDTO()
    {
    }
}
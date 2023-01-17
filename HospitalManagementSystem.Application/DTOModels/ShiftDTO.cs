namespace HospitalManagementSystem.Application.DTOModels;

public class ShiftDTO
{
    public DateTime Date { get; set; }
    public List<EmployeeDTO> Users { get; set; }
}
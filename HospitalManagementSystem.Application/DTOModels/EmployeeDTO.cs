using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Application.DTOModels;

public class EmployeeDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Pesel { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public byte[] Password { get; set; }
    public Role Role { get; set; }
    public DoctorPrivilegesDTO? DoctorPrivileges { get; set; }

    public EmployeeDTO()
    {
        
    }
}
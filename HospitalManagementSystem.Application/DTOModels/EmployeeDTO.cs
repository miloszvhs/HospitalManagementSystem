namespace HospitalManagementSystem.Application.DTOModels;

public class EmployeeDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public byte[] Password { get; set; }
    public int Role { get; set; }
}
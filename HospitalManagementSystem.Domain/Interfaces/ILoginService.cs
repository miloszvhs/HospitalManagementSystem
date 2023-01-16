using System.Text.Json;
using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Domain.Interfaces;

public interface ILoginService
{
    Employee Login();
    
}



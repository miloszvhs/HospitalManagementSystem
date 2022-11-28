using HospitalManagementSystem.Domain.Abstractions;
using HospitalManagementSystem.Domain.ValueObjects;

namespace HospitalManagementSystem.Domain.Entities;

public class Admin : User
{
    public Admin(HospitalManagementSystemUsername username, HospitalManagementSystemPassword password) : base(username, password)
    {
    }
}
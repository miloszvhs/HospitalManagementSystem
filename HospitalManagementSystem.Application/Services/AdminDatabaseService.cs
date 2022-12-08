using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Application.Services;

public class AdminDatabaseService : HospitalManagementSystemBaseDb<Admin>
{
    private readonly XMLService<Admin> _xmlService;

    public AdminDatabaseService()
    {
        _xmlService = new(this, "admins.xml");
    }
}
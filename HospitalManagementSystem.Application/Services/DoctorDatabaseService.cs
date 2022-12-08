using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Application.Services;

public class DoctorDatabaseService : HospitalManagementSystemBaseDb<Doctor>
{
    private readonly XMLService<Doctor> _xmlService;

    public DoctorDatabaseService()
    {
        _xmlService = new(this, "doctors.xml");
    }
}
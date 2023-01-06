using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

namespace HospitalManagementSystem.Application.Services;

public class PWZNumberService : IPWZNumberService
{
    private readonly IDatabaseService _database;

    public PWZNumberService(IDatabaseService database)
    {
        _database = database;
    }

    private Random random { get; } = new();

    public int GetNewPWZ()
    {
        int pwz;

        while (true)
        {
            pwz = GeneratePWZ();

            if (_database.Users.Where(x => x.DoctorPrivileges is not null).FirstOrDefault(x => x.DoctorPrivileges.Pwz == pwz) is null) break;
        }

        return pwz;
    }

    private int GeneratePWZ()
    {
        var pwz = new int[7];
        var sum = 0;

        for (var i = 1; i < pwz.Length; i++)
        {
            pwz[i] = random.Next(0, 9);
            sum += pwz[i] * i;
        }

        var controlNumber = sum % 11;

        pwz[0] = controlNumber;

        var pwzAsString = string.Join("", pwz);
        var result = ((IConvertible)pwzAsString).ToInt32(default);

        return result;
    }
}
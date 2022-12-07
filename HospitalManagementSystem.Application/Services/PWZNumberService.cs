namespace HospitalManagementSystem.Application.Services;

public class PWZNumberService
{
    private readonly DoctorDatabaseService _database;
    private Random random { get; } = new();
    
    public PWZNumberService(DoctorDatabaseService database)
    {
        _database = database;
    }
    
    public int GetNewPWZ()
    {
        int pwz;
        
        while (true)
        {
            pwz = GeneratePWZ();

            if (!_database.GetAllUsers().Exists(x => x.Pwz == pwz))
            {
                break;
            }
        }

        return pwz;
    }

    private int GeneratePWZ()
    {
        var pwz = new int[7];
        var sum = 0;
        
        for (int i = 1; i < pwz.Length; i++)
        {
            pwz[i] = random.Next(0, 9);
            sum += (pwz[i] * i);
        }

        var controlNumber = sum % 11;
        
        pwz[0] = controlNumber;

        var pwzAsString = pwz.ToString();
        var result = ((IConvertible)pwzAsString).ToInt32(default);
        
        return result;
    }
}
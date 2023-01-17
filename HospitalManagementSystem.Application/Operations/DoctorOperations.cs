using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

namespace HospitalManagementSystem.Application.Operations;

public class DoctorOperations
{
    private readonly IDatabaseService _database;
    private readonly Employee _employee;
    private readonly IMenuActionService _menuActionService;
    private readonly IPWZNumberService _pwzNumberService;
    private readonly IShiftService _shiftService;

    public DoctorOperations(IDatabaseService database,
        IMenuActionService menuActionService,
        IPWZNumberService pwzNumberService,
        IShiftService shiftService,
        Employee employee)
    {
        _database = database;
        _menuActionService = menuActionService;
        _pwzNumberService = pwzNumberService;
        _shiftService = shiftService;
        _employee = employee;
    }

    public void Run()
    {
        while (true)
        {
            _menuActionService.DrawMenuViewByMenuType("Doctor");

            var input = Console.ReadKey();
            Console.WriteLine();

            switch (input.KeyChar)
            {
                case '1':
                    _shiftService.Run();
                    break;
                case '2':
                    ShowUsers();
                    break;
                case '3':
                    return;
                default:
                    Console.WriteLine("Niepoprawny input");
                    break;
            }
        }
    }

    private void ShowUsers()
    {
        Console.Write("Numer\tId\tTyp\t\tImie\t\tPWZ\tSpecjalizacja\n");

        foreach (var (user, index) in _database.Items.Select((x, y) => (x, y + 1)))
        {
            switch (user.Rola)
            {
                case Role.Administrator:
                    Console.WriteLine(
                        $"{index}.\t{user.Id}\t{string.Format("{0, -10}", user.Rola)}\t{string.Format("{0, -10}", user.Name.Value)}\t-\t-");
                    break;
                case Role.Lekarz:
                    Console.WriteLine(
                        $"{index}.\t{user.Id}\t{user.Rola}\t\t{string.Format("{0, -15}", user.Name.Value)}\t{user.DoctorPrivileges.Pwz.Value}\t{user.DoctorPrivileges.Specjalizacja}");
                    break;
                case Role.Pracownik:
                    Console.WriteLine(
                        $"{index}.\t{user.Id}\t{user.Rola}\t{string.Format("{0, -10}", user.Name.Value)}\t-\t-");
                    break;
            } 
        }
    }
}
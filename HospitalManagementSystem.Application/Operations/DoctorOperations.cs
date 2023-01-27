using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using Spectre.Console;

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
        _menuActionService.DrawUsers(_database.Items);
    }
}
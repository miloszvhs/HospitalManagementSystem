using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

namespace HospitalManagementSystem.Application.Operations;

public class EmployeeOperations
{
    private readonly IDatabaseService _database;
    private readonly Employee _employee;
    private readonly IMenuActionService _menuActionService;
    private readonly IShiftService _shiftService;

    public EmployeeOperations(IDatabaseService database,
        IMenuActionService menuActionService,
        IShiftService shiftService,
        Employee employee)
    {
        _database = database;
        _menuActionService = menuActionService;
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
    }
}
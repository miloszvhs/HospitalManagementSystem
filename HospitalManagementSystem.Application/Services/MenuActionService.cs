using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using Spectre.Console;

namespace HospitalManagementSystem.Application.Services;

public class MenuActionService : IMenuActionService
{
    private readonly List<MenuAction> _menuActions = new();

    public MenuActionService()
    {
        InitializeMenu();
    }

    public void DrawMenuViewByMenuType(string menuType)
    {
        var menu = _menuActions.SingleOrDefault(x => x.MenuType == menuType);
        
        if(menu is not null)
        {
            AnsiConsole.Write(menu.Table);
        }
    }

    public void DrawUsers(List<Employee> users)
    {
        var table = new Table()
            .AddColumn("[dodgerblue3]Numer[/]")
            .AddColumn("[deepskyblue3]Id[/]")
            .AddColumn("[dodgerblue3]Typ[/]")
            .AddColumn("[deepskyblue3]Imie[/]")
            .AddColumn("[dodgerblue3]PWZ[/]")
            .AddColumn("[deepskyblue3]Specjalizacja[/]");

        foreach (var (user, index) in users.Select((x, y) => (x, y + 1)))
        {
            var doctorPrivileges = user.DoctorPrivileges is not null
                ? user.DoctorPrivileges.DoctorSpecialization.ToString()
                : "-";
            var pwzNumber = user.DoctorPrivileges is not null ? user.DoctorPrivileges.Pwz.Value.ToString() : "-";

            table.AddRow($"{index}.", $"{user.Id}", $"{user.Role}", $"{user.Name.Value}", $"{doctorPrivileges}",
                $"{pwzNumber}");
        }

        AnsiConsole.Write(table);
    }
    
    public void DrawShifts(List<Shift> shifts)
    {
        var table = new Table()
            .AddColumn("[dodgerblue3]Id[/]")
            .AddColumn("[deepskyblue3]Nazwa uzytkownika[/]")
            .AddColumn("[dodgerblue3]Imie[/]")
            .AddColumn("[deepskyblue3]Nazwisko[/]")
            .AddColumn("[dodgerblue3]Specjalizacja[/]");
        
        foreach (var shift in shifts.Where(x => x.Date.Date >= DateTime.Now.Date))
        {
            table.AddRow($"{shift.Date.ToShortDateString()}", "[invert]-----[/]", "[invert]-----[/]", "[invert]-----[/]", "[invert]-----[/]");

            foreach (var employee in shift.Users)
            {
                var doctorPrivileges = employee.DoctorPrivileges is null
                    ? "-"
                    : employee.DoctorPrivileges.DoctorSpecialization.ToString();
        
                table.AddRow($"{employee.Id}", $"{employee.Username.Value}", $"{employee.Name.Value}", $"{employee.LastName.Value}", $"{doctorPrivileges}");
            }
            table.AddEmptyRow();
        }
        AnsiConsole.Write(table);
    }
    
    private void InitializeMenu()
    {
        _menuActions.Add(new MenuAction(new Table()
                .AddColumn("[dodgerblue3]Numer[/]")
                .AddColumn("[deepskyblue3]Akcja[/]")
                .AddRow("1", "Logowanie")
                .AddRow("2", "Rejestracja")
                .AddRow("3", "Wyjście")
            , "MainMenu"));

        _menuActions.Add(new MenuAction(new Table()
                .AddColumn("[dodgerblue3]Numer[/]")
                .AddColumn("[deepskyblue3]Akcja[/]")
                .AddRow("1", "Dyżury")
                .AddRow("2", "Pokaż użytkowników")
                .AddRow("3", "Dodaj użytkownika")
                .AddRow("4", "Usuń użytkownika")
                .AddRow("5", "Zmień dane użytkownika")
                .AddRow("6", "Wylogowanie"),
            "Admin"));

        _menuActions.Add(new MenuAction(new Table()
                .AddColumn("[dodgerblue3]Numer[/]")
                .AddColumn("[deepskyblue3]Akcja[/]")
                .AddRow("1", "Dyżury")
                .AddRow("2", "Pokaż użytkowników")
                .AddRow("3", "Wylogowanie")
            , "Doctor"));

        _menuActions.Add(new MenuAction(new Table()
                .AddColumn("[dodgerblue3]Numer[/]")
                .AddColumn("[deepskyblue3]Akcja[/]")
                .AddRow("1", "Dyżury")
                .AddRow("2", "Pokaż użytkowników")
                .AddRow("3", "Wylogowanie")
            , "Employee"));

        _menuActions.Add(new MenuAction(new Table()
                .AddColumn("[dodgerblue3]Numer[/]")
                .AddColumn("[deepskyblue3]Akcja[/]")
                .AddRow("1", "Pokaż dyżury")
                .AddRow("2", "Dodaj dyżur")
                .AddRow("3", "Usuń dyżur")
                .AddRow("4", "Zmień dyżur")
                .AddRow("5", "Powrót")
            , "ShiftsForAdministrator"));

        _menuActions.Add(new MenuAction(new Table()
                .AddColumn("[dodgerblue3]Numer[/]")
                .AddColumn("[deepskyblue3]Akcja[/]")
                .AddRow("1", "Pokaż dyżury")
                .AddRow("2", "Powrót")
            , "ShiftsForOthers"));

        _menuActions.Add(new MenuAction(new Table()
                .AddColumn("[dodgerblue3]Numer[/]")
                .AddColumn("[deepskyblue3]Akcja[/]")
                .AddRow("1", "Kardiolog")
                .AddRow("2", "Urolog")
                .AddRow("3", "Laryngolog")
                .AddRow("4", "Neurolog")
            , "Specialization"));

        _menuActions.Add(new MenuAction(new Table()
                .AddColumn("[dodgerblue3]Numer[/]")
                .AddColumn("[deepskyblue3]Akcja[/]")
                .AddRow("1", "Pielęgniarka")
                .AddRow("2", "Lekarz")
                .AddRow("3", "Admin")
            , "Roles"));
    }
}
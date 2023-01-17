using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

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
        foreach (var menu in _menuActions)
            if (menu.MenuType == menuType)
                Console.WriteLine($"({menu.Id}) {menu.Name}");
    }

    private void InitializeMenu()
    {
        _menuActions.Add(new MenuAction(1, "Logowanie", "MainMenu"));
        _menuActions.Add(new MenuAction(2, "Rejestracja", "MainMenu"));
        _menuActions.Add(new MenuAction(3, "Wyjście", "MainMenu"));

        _menuActions.Add(new MenuAction(1, "Dyżury", "Admin"));
        _menuActions.Add(new MenuAction(2, "Pokaż użytkowników", "Admin"));
        _menuActions.Add(new MenuAction(3, "Dodaj użytkownika", "Admin"));
        _menuActions.Add(new MenuAction(4, "Usuń użytkownika", "Admin"));
        _menuActions.Add(new MenuAction(5, "Zmień dane użytkownika", "Admin"));
        _menuActions.Add(new MenuAction(6, "Wylogowanie", "Admin"));

        _menuActions.Add(new MenuAction(1, "Dyżury", "Doctor"));
        _menuActions.Add(new MenuAction(2, "Pokaż użytkowników", "Doctor"));
        _menuActions.Add(new MenuAction(3, "Wylogowanie", "Doctor"));

        _menuActions.Add(new MenuAction(1, "Dyżury", "Employee"));
        _menuActions.Add(new MenuAction(2, "Pokaż użytkowników", "Employee"));
        _menuActions.Add(new MenuAction(3, "Wylogowanie", "Employee"));

        _menuActions.Add(new MenuAction(1, "Pokaż dyżury", "ShiftsForAdministrator"));
        _menuActions.Add(new MenuAction(2, "Dodaj dyżur", "ShiftsForAdministrator"));
        _menuActions.Add(new MenuAction(3, "Usuń dyżur", "ShiftsForAdministrator"));
        _menuActions.Add(new MenuAction(4, "Powrót", "ShiftsForAdministrator"));
        
        _menuActions.Add(new MenuAction(1, "Pokaż dyżury", "ShiftsForOthers"));
        _menuActions.Add(new MenuAction(2, "Powrót", "ShiftsForOthers"));

        _menuActions.Add(new MenuAction(1, "Kardiolog", "Specialization"));
        _menuActions.Add(new MenuAction(2, "Urolog", "Specialization"));
        _menuActions.Add(new MenuAction(3, "Laryngolog", "Specialization"));
        _menuActions.Add(new MenuAction(4, "Neurolog", "Specialization"));

        _menuActions.Add(new MenuAction(0, "Pielęgniarka", "Roles"));
        _menuActions.Add(new MenuAction(1, "Lekarz", "Roles"));
        _menuActions.Add(new MenuAction(2, "Admin", "Roles"));
    }
}
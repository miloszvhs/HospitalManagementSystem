using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Application.Services;

public class MenuActionService
{
    public Employee Employee { get; }
    private List<MenuAction> _menuActions = new();
    
    public MenuActionService()
    {
        InitializeMenu();
    }
    
    public void DrawMenuViewByMenuType(string menuType)
    {
        foreach (var menu in _menuActions)
        {
            if (menu.MenuType == menuType)
            {
                Console.WriteLine($"({menu.Id}) {menu.Name}");
            }
        }
    }
    
    private void InitializeMenu()
    {
        _menuActions.Add(new MenuAction(1, "Logowanie", "MainMenu"));
        _menuActions.Add(new MenuAction(2, "Rejestracja", "MainMenu"));
        
        _menuActions.Add(new MenuAction(1, "Dyżury", "Admin"));
        _menuActions.Add(new MenuAction(2, "Pokaż użytkowników", "Admin"));
        _menuActions.Add(new MenuAction(3, "Dodaj użytkownika", "Admin"));
        _menuActions.Add(new MenuAction(4, "Usuń użytkownika", "Admin"));
        _menuActions.Add(new MenuAction(5, "Wylogowanie", "Admin"));

        _menuActions.Add(new MenuAction(1, "Dyżury", "Doctor"));
        _menuActions.Add(new MenuAction(2, "Koniec", "Doctor"));
        _menuActions.Add(new MenuAction(3, "Koniec", "Doctor"));
        _menuActions.Add(new MenuAction(4, "Koniec", "Doctor"));
        _menuActions.Add(new MenuAction(5, "Wylogowanie", "Doctor"));
        
        _menuActions.Add(new MenuAction(1, "Dyżury", "Employee"));
        _menuActions.Add(new MenuAction(2, "Koniec", "Employee"));
        _menuActions.Add(new MenuAction(3, "Koniec", "Employee"));
        _menuActions.Add(new MenuAction(4, "Koniec", "Employee"));
        _menuActions.Add(new MenuAction(5, "Wylogowanie", "Employee"));
        
        _menuActions.Add(new MenuAction(1, "Koniec", "Shifts"));
        _menuActions.Add(new MenuAction(2, "Koniec", "Shifts"));
        _menuActions.Add(new MenuAction(3, "Koniec", "Shifts"));
        _menuActions.Add(new MenuAction(4, "Koniec", "Shifts"));
        _menuActions.Add(new MenuAction(5, "Powrót", "Shifts"));
    }
}
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
        _menuActions.Add(new MenuAction(3, "Remove recipe", "Admin"));
        _menuActions.Add(new MenuAction(4, "Edit recipe", "Admin"));
        _menuActions.Add(new MenuAction(5, "Koniec", "Admin"));

        _menuActions.Add(new MenuAction(1, "Add ingredients", "EditMenu"));
        _menuActions.Add(new MenuAction(2, "Remove and edit all ingredients", "EditMenu"));
        _menuActions.Add(new MenuAction(3, "Remove ingredient", "EditMenu"));
        _menuActions.Add(new MenuAction(4, "Edit specified ingredient", "EditMenu"));
        _menuActions.Add(new MenuAction(5, "Change description", "EditMenu"));
        _menuActions.Add(new MenuAction(6, "Leave", "EditMenu"));
    }
}
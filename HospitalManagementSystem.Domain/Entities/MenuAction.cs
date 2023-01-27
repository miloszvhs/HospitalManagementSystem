using Spectre.Console;

namespace HospitalManagementSystem.Domain.Entities;

public class MenuAction
{
    public Table Table { get; }
    public string MenuType { get; }

    public MenuAction(Table table, string menuType)
    {
        Table = table;
        MenuType = menuType;
    }
}
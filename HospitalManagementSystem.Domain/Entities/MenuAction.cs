namespace HospitalManagementSystem.Domain.Entities;

public class MenuAction
{
    public int Id { get; }
    public string Name { get; }
    public string MenuType { get; }

    public MenuAction(int id, string name, string menuType)
    {
        Id = id;
        Name = name;
        MenuType = menuType;
    }
}
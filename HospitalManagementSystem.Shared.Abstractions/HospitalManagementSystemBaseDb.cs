using HospitalManagementSystem.Shared.Abstractions.Exceptions;

namespace HospitalManagementSystem.Shared.Abstractions;

public abstract class HospitalManagementSystemBaseDb<T> where T : BaseEntity
{
    public List<T> Items { get; set; } = new();

    public void Add(T item) => Items.Add(item);
    
    public void Update(T item)
    {
        var entity = Items.FirstOrDefault(x => x.Id == item.Id);

        if (entity is not null)
        {
            UpdateObject(entity, item);
        }
    }

    public void Remove(T item)
    {
        var entity = Items.FirstOrDefault(x => x.Id == item.Id);

        if (entity is not null)
        {
            Items.Remove(entity);
        }
        else
        {
            throw new CannotFindUserException(item.Id);
        }
    }

    public T Get(int id)
    {
        var entity = Items.FirstOrDefault(x => x.Id == id);
        
        if(entity is not null)
        {
            return entity;
        }

        return null;
    }

    public int GetLastId()
    {
        if (Items.Any())
        {
            var id = Items.OrderBy(x => x.Id).LastOrDefault().Id;
            return id;   
        }

        return 1;
    }
        
    private void UpdateObject(object oldObject, object newObject)
    {
        var type = oldObject.GetType();
        var fields = type.GetProperties();
        
        foreach (var field in fields)
        {
            var newValue = field.GetValue(newObject);
            field.SetValue(oldObject, newValue);
        }
    }
}
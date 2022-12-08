using HospitalManagementSystem.Shared.Abstractions.Exceptions;

namespace HospitalManagementSystem.Shared.Abstractions;

public abstract class HospitalManagementSystemBaseDb<T> where T : BaseEntity
{
    public List<T> Users { get; set; } = new();

    public void AddUser(T user) => Users.Add(user);
    
    public void UpdateUser(T user)
    {
        var entity = Users.FirstOrDefault(x => x.Id == user.Id);

        if (entity is not null)
        {
            entity = user;
        }
    }

    public void RemoveUser(T user)
    {
        var entity = Users.FirstOrDefault(x => x.Id == user.Id);

        if (entity is not null)
        {
            Users.Remove(entity);
        }
        else
        {
            throw new CannotFindUserException(user.Id);
        }
    }

    public T GetUser(int id)
    {
        var user = Users.FirstOrDefault(x => x.Id == id);
        
        if(user is not null)
        {
            return user;
        }

        return null;
    }
}
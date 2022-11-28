using HospitalManagementSystem.Domain.Abstractions;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Exceptions;

namespace HospitalManagementSystem.Infrastructure.Database;

public class HospitalManagementSystemDb
{
    public IEnumerable<User> Users => _users;

    private readonly HashSet<User> _users = new();

    public void AddUser(User user) => _users.Add(user);
    public void UpdateUser(User user)
    {
        var entity = _users.FirstOrDefault(x => x.Username == user.Username);

        if (entity is not null)
        {
            entity = user;
        }
        else
        {
            throw new CannotFindUserException(user.Username);
        }
    }
}
using HospitalManagementSystem.Domain.Abstractions;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.ValueObjects;
using HospitalManagementSystem.Infrastructure.Database;

namespace HospitalManagementSystem.Application.Services;

public class UsersLoginService
{
    private readonly HospitalManagementSystemDb _database;

    public UsersLoginService(HospitalManagementSystemDb database)
    {
        _database = database;
    }
    
    public User Login()
    {
        var usernameInput = WriteAndRead("Login: ");
        
        HospitalManagementSystemUsername login = new(usernameInput);

        var passwordInput = WriteAndRead("Password: ");

        HospitalManagementSystemPassword userPassword = new(passwordInput);
        
        var user = new User(login, userPassword);

        var succeeded = CheckIfValidationSucceeded(user);

        if(succeeded)
        {
            return user;
        }

        return null;
    }

    private bool CheckIfValidationSucceeded(User user) 
        => _database.Users.Any(x => x.Username == user.Username 
                                        && x.Password == user.Password);
    
    private string WriteAndRead(string value)
    {
        Console.Write(value);
        
        var result = Console.ReadLine();
        
        return result;
    }
}

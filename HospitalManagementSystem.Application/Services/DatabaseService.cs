using AutoMapper;
using HospitalManagementSystem.Application.DTOModels;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Infrastructure.Database;

namespace HospitalManagementSystem.Application.Services;

public class DatabaseService : EmployeeHospitalManagementSystemDb, IDatabaseService
{
    private XMLService<Employee, EmployeeDTO> _xmlService;

    
    public DatabaseService()
    {
        var mapperConfiguration = InitializeMapperConfiguration();
        var mapperConfigurationDTO = InitializeMapperConfigurationDTO();
        
        _xmlService = new XMLService<Employee, EmployeeDTO>(this, "employees.xml", "Employees", mapperConfiguration,
            mapperConfigurationDTO);
    }

    public void RestoreFromXmlFile()
    {
        _xmlService.RestoreFromXmlFile();
    }

    public void SaveToXmlFile()
    {
        _xmlService.SaveToXmlFile();
    }

    public void AddEmployee(Employee employee)
    {
        AddUser(employee);
    }

    public int UpdateEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public int RemoveEmployee(int id)
    {
        var user = GetUser(id);

        if (user is not null)
        {
            user.IsDeleted = true;
            return 1;
        }

        return 0;
    }
    
    public List<Employee> GetAllEmployees()
    {
        var users = Users;
        return users;
    }

    public Employee GetEmployee(int id)
    {
        var employee = Users.FirstOrDefault(x => x.Id == id);

        if (employee is not null)
        {
            return employee;
        }

        return null;
    }

    public int GetLastId()
    {
        var lastId = GetLastId();
        return lastId;
    }
    
    public Employee GetUser(int id)
    {
        var user = GetUser(id);

        return user;
    }

    public int DeleteUser(int id)
    {
        var user = GetUser(id);

        if (user is not null)
        {
            RemoveUser(user);
            return 1;
        }

        return 0;
    }

    public int ChangeUser(Employee employee)
    {
        var user = GetUser(employee.Id);

        if (user is not null)
        {
            user = employee;
            return 1;
        }

        return 0;
    }
    
    private MapperConfiguration InitializeMapperConfiguration()
    {
        return new MapperConfiguration(cfg =>
            cfg.CreateMap<Employee, EmployeeDTO>()
                .ForMember(x => x.Name, s => s.MapFrom(d => d.Name.Value))
                .ForMember(x => x.Password, s => s.MapFrom(d => d.Password.Value))
                .ForMember(x => x.Username, s => s.MapFrom(d => d.Username.Value))
                .ForMember(x => x.LastName, s => s.MapFrom(d => d.LastName.Value))
                .ForMember(x => x.Role, s => s.MapFrom(d => d.Rola))
                .ReverseMap());
    }

    private MapperConfiguration InitializeMapperConfigurationDTO()
    {
        return new MapperConfiguration(cfg =>
            cfg.CreateMap<Employee, EmployeeDTO>()
                .ForMember(x => x.Name, s => s.MapFrom(d => d.Name.Value))
                .ForMember(x => x.Password, s => s.MapFrom(d => d.Password.Value))
                .ForMember(x => x.Username, s => s.MapFrom(d => d.Username.Value))
                .ForMember(x => x.LastName, s => s.MapFrom(d => d.LastName.Value))
                .ForMember(x => x.Role, s => s.MapFrom(d => d.Rola)));
    }
}
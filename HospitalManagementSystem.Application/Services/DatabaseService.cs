using AutoMapper;
using HospitalManagementSystem.Application.DTOModels;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Domain.ValueObjects;
using HospitalManagementSystem.Infrastructure.Database;

namespace HospitalManagementSystem.Application.Services;

public class DatabaseService : HospitalManagementSystemDb, IDatabaseService
{
    private readonly XMLService _xmlService;
    private readonly IPasswordHasherService _passwordHasherService; 
    
    public DatabaseService(IPasswordHasherService passwordHasherService)
    {
        _passwordHasherService = passwordHasherService;
        var mapperConfigurationForDTO = InitializeMapperConfigurationFromDTO();
        var mapperConfiguration = InitializeMapperConfiguration();
        
        _xmlService = new XMLService(this, "employees.xml", "Employees", mapperConfigurationForDTO,
            mapperConfiguration);
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
            return 1;
        }

        return 0;
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
        var id = base.GetLastId();
        return id;
    }

    public void Seed()
    {
        AddUser(new Employee(new HospitalManagementSystemUsername("Admin"),
            new HospitalManagementSystemPassword(_passwordHasherService.HashPassword("Admin")),
            new HospitalManagementSystemId(1),
            new HospitalManagementSystemName("Admin"),
            new HospitalManagementSystemName("Admin"),
            Role.Administrator));
        
        _xmlService.SaveToXmlFile();
    }

    public Employee GetUser(int id)
    {
        var user = base.GetUser(id);
        return user;
    }

    public int DeleteUser(int id)
    {
        var user = base.GetUser(id);

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
    
    private MapperConfiguration InitializeMapperConfigurationFromDTO()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<EmployeeDTO, Employee>()
                .ForMember(x => x.Name, s => s.MapFrom(d => d.Name))
                .ForMember(x => x.Password, s => s.MapFrom(d => d.Password))
                .ForMember(x => x.Username, s => s.MapFrom(d => d.Username))
                .ForMember(x => x.LastName, s => s.MapFrom(d => d.LastName))
                .ForMember(x => x.Rola, s => s.MapFrom(d => d.Role))
                .ForMember(x => x.DoctorPrivileges, s => s.MapFrom(d => d.DoctorPrivileges));
            cfg.CreateMap<DoctorPrivilegesDTO, DoctorPrivileges>();
        });
    }

    private MapperConfiguration InitializeMapperConfiguration()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Employee, EmployeeDTO>()
                .ForMember(x => x.Name, s => s.MapFrom(d => d.Name.Value))
                .ForMember(x => x.Password, s => s.MapFrom(d => d.Password.Value))
                .ForMember(x => x.Username, s => s.MapFrom(d => d.Username.Value))
                .ForMember(x => x.LastName, s => s.MapFrom(d => d.LastName.Value))
                .ForMember(x => x.Role, s => s.MapFrom(d => d.Rola))
                .ForMember(x => x.DoctorPrivileges, s => s.MapFrom(d => d.DoctorPrivileges));
            cfg.CreateMap<DoctorPrivileges, DoctorPrivilegesDTO>();
        });
    }
}
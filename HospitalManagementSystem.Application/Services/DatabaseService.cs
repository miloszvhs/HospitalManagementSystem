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
        Add(employee);
    }

    public int UpdateEmployee(Employee employee)
    {
        var entity = GetUser(employee.Id);

        if (entity is not null)
        {
            Update(employee);
            return employee.Id;
        }

        return -1;
    }

    public int RemoveEmployee(int id)
    {
        var user = GetUser(id);

        if (user is not null)
        {
            Remove(user);
            return user.Id;
        }

        return 0;
    }

    public Employee GetEmployee(int id)
    {
        var employee = Items.FirstOrDefault(x => x.Id == id);

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
        Add(new Employee(new HospitalManagementSystemUsername("Admin"),
            new HospitalManagementSystemPassword(_passwordHasherService.HashPassword("Admin")),
            new HospitalManagementSystemPesel("12345678910"),
            new HospitalManagementSystemId(1),
            new HospitalManagementSystemName("Admin"),
            new HospitalManagementSystemName("Admin"),
            Role.Administrator));
        
        _xmlService.SaveToXmlFile();
    }

    public Employee GetUser(int id)
    {
        var user = base.Get(id);
        return user;
    }

    private MapperConfiguration InitializeMapperConfigurationFromDTO()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<EmployeeDTO, Employee>()
                .ConstructUsing((EmployeeDTO src) => new Employee(src.Username,
                    new HospitalManagementSystemPassword(src.Password), src.Pesel, src.Id, src.Name, src.LastName, src.Role));
            cfg.CreateMap<DoctorPrivilegesDTO, DoctorPrivileges>()
                .ForMember(x => x.Pwz, s => s.MapFrom(d => new HospitalManagementSystemPWZ(d.Pwz)))
                .ForMember(x => x.Specjalizacja, s => s.MapFrom(d => d.Specjalizacja));
        });
    }

    private MapperConfiguration InitializeMapperConfiguration()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Employee, EmployeeDTO>()
                .ForMember(x => x.Name, s => s.MapFrom(d => d.Name.Value))
                .ForMember(x => x.Password, s => s.MapFrom(d => d.Password.Value))
                .ForMember(x => x.Pesel, s => s.MapFrom(d => d.Pesel.Value))
                .ForMember(x => x.Username, s => s.MapFrom(d => d.Username.Value))
                .ForMember(x => x.LastName, s => s.MapFrom(d => d.LastName.Value))
                .ForMember(x => x.Role, s => s.MapFrom(d => d.Rola))
                .ForMember(x => x.DoctorPrivileges, s => s.MapFrom(d => d.DoctorPrivileges));
            cfg.CreateMap<DoctorPrivileges, DoctorPrivilegesDTO>()
                .ForMember(x => x.Pwz, s => s.MapFrom(d => d.Pwz.Value))
                .ForMember(x => x.Specjalizacja, s => s.MapFrom(d => d.Specjalizacja));
        });
    }
}
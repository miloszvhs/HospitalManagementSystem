using AutoMapper;
using HospitalManagementSystem.Application.DTOModels;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Application.Services;

public class EmployeeDatabaseService : HospitalManagementSystemBaseDb<Employee>, IDatabaseService<Employee>
{
    private readonly XMLService<Employee, EmployeeDTO> _xmlService;

    public EmployeeDatabaseService()
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
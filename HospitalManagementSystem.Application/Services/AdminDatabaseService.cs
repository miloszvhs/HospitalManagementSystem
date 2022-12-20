using AutoMapper;
using HospitalManagementSystem.Application.DTOModels;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Application.Services;

public class AdminDatabaseService : HospitalManagementSystemBaseDb<Admin>, IDatabaseService<Admin>
{
    private readonly XMLService<Admin, AdminDTO> _xmlService;

    public AdminDatabaseService()
    {
        var mapperConfiguration = InitializeMapperConfiguration();

        var mapperConfigurationDTO = InitializeMapperConfigurationDTO();
        
        _xmlService = new(this, "admins.xml", "Admins", mapperConfiguration, mapperConfigurationDTO);
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
            cfg.CreateMap<Admin, AdminDTO>()
                .ForMember(x => x.Name, s => s.MapFrom(d => d.Name.Value))
                .ForMember(x => x.Password, s => s.MapFrom(d => d.Password.Value))
                .ForMember(x => x.Username, s => s.MapFrom(d => d.Username.Value))
                .ForMember(x => x.LastName, s => s.MapFrom(d => d.LastName.Value))
                .ReverseMap());
    }
    
    private MapperConfiguration InitializeMapperConfigurationDTO()
    {
        return new MapperConfiguration(cfg =>
            cfg.CreateMap<Admin, AdminDTO>()
                .ForMember(x => x.Name, s => s.MapFrom(d => d.Name.Value))
                .ForMember(x => x.Password, s => s.MapFrom(d => d.Password.Value))
                .ForMember(x => x.Username, s => s.MapFrom(d => d.Username.Value))
                .ForMember(x => x.LastName, s => s.MapFrom(d => d.LastName.Value)));
    }
}
using AutoMapper;
using HospitalManagementSystem.Application.DTOModels;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Application.Services;

public class DoctorDatabaseService : HospitalManagementSystemBaseDb<Doctor>, IDatabaseService<Doctor>
{
    private readonly XMLService<Doctor, DoctorDTO> _xmlService;

    public DoctorDatabaseService()
    {
        var mapperConfiguration = InitializeMapperConfiguration();

        var mapperConfigurationDTO = InitializeMapperConfigurationDTO();

        _xmlService = new XMLService<Doctor, DoctorDTO>(this, "doctors.xml", "Doctors", mapperConfiguration,
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
            cfg.CreateMap<Doctor, DoctorDTO>()
                .ForMember(x => x.Name, s => s.MapFrom(d => d.Name.Value))
                .ForMember(x => x.Password, s => s.MapFrom(d => d.Password.Value))
                .ForMember(x => x.Username, s => s.MapFrom(d => d.Username.Value))
                .ForMember(x => x.LastName, s => s.MapFrom(d => d.LastName.Value))
                .ForMember(x => x.Pwz, s => s.MapFrom(d => d.Pwz.Value))
                .ForMember(x => x.Specjalizacja, s => s.MapFrom(d => d.Specjalizacja))
                .ForMember(x => x.Role, s => s.MapFrom(d => d.Rola))
                .ReverseMap());
    }

    private MapperConfiguration InitializeMapperConfigurationDTO()
    {
        return new MapperConfiguration(cfg =>
            cfg.CreateMap<Doctor, DoctorDTO>()
                .ForMember(x => x.Name, s => s.MapFrom(d => d.Name.Value))
                .ForMember(x => x.Password, s => s.MapFrom(d => d.Password.Value))
                .ForMember(x => x.Username, s => s.MapFrom(d => d.Username.Value))
                .ForMember(x => x.LastName, s => s.MapFrom(d => d.LastName.Value))
                .ForMember(x => x.Pwz, s => s.MapFrom(d => d.Pwz.Value))
                .ForMember(x => x.Specjalizacja, s => s.MapFrom(d => d.Specjalizacja))
                .ForMember(x => x.Role, s => s.MapFrom(d => d.Rola)));
    }
}
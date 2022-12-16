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
        var mapperConfiguration = new MapperConfiguration(cfg =>
            cfg.CreateMap<Doctor, DoctorDTO>()
                .ForMember(x => x.Name, s => s.MapFrom(d => d.Name.Value))
                .ForMember(x => x.Password, s => s.MapFrom(d => d.Password.Value))
                .ForMember(x => x.Username, s => s.MapFrom(d => d.Username.Value))
                .ForMember(x => x.LastName, s => s.MapFrom(d => d.LastName.Value))
                .ForMember(x => x.Pwz, s => s.MapFrom(d => d.Pwz.Value))
                .ForMember(x => x.Specjalizacja, s => s.MapFrom(d => d.Specjalizacja))
                .ReverseMap());
        
        var mapperConfigurationDTO = new MapperConfiguration(cfg =>
            cfg.CreateMap<Doctor, DoctorDTO>()
                .ForMember(x => x.Name, s => s.MapFrom(d => d.Name.Value))
                .ForMember(x => x.Password, s => s.MapFrom(d => d.Password.Value))
                .ForMember(x => x.Username, s => s.MapFrom(d => d.Username.Value))
                .ForMember(x => x.LastName, s => s.MapFrom(d => d.LastName.Value))
                .ForMember(x => x.Pwz, s => s.MapFrom(d => d.Pwz.Value))
                .ForMember(x => x.Specjalizacja, s => s.MapFrom(d => d.Specjalizacja)));
        
        _xmlService = new(this, "doctors.xml", "Doctors", mapperConfiguration, mapperConfigurationDTO);
    }
    
    public void RestoreFromXmlFile()
    {
        _xmlService.RestoreFromXmlFile();
    }
    
    public void SaveToXmlFile()
    {
        _xmlService.SaveToXmlFile();
    }
}
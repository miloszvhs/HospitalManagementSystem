using AutoMapper;
using HospitalManagementSystem.Application.DTOModels;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;
using HospitalManagementSystem.Domain.ValueObjects;
using HospitalManagementSystem.Infrastructure.Database;

namespace HospitalManagementSystem.Application.Services;

public class ShiftDatabaseService : HospitalManagementSystemShiftDb, IShiftDatabaseService
{
    private readonly XMLShiftService _xmlShiftService;

    public ShiftDatabaseService()
    {
        var mapperConfigurationForDTO = MapperConfigurationFromDTO();
        var mapperConfiguration = MapperConfiguration();

        _xmlShiftService = new XMLShiftService(this, "shifts.xml", "Shifts", mapperConfigurationForDTO,
            mapperConfiguration);
    }

    public void RestoreFromXmlFile()
    {
        _xmlShiftService.RestoreFromXmlFile();
    }

    public void SaveToXmlFile()
    {
        _xmlShiftService.SaveToXmlFile();
    }

    public void AddShift(Shift shift)
    {
        Add(shift);
    }

    public int UpdateShift(Shift shift)
    {
        var entity = Get(shift.Id);

        if (shift is not null)
        {
            Update(shift);
            return shift.Id;
        }

        return -1;
    }

    public int RemoveShift(int id)
    {
        var shift = Get(id);

        if (shift is not null)
        {
            Remove(shift);
            return shift.Id;
        }

        return -1;
    }

    public Shift GetShift(int id)
    {
        var shift = Items.FirstOrDefault(x => x.Id == id);

        if (shift is not null) return shift;

        return null;
    }

    private MapperConfiguration MapperConfigurationFromDTO()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ShiftDTO, Shift>()
                .ForMember(x => x.Date, s => s.MapFrom(d => d.Date))
                .ForMember(x => x.Users, s => s.MapFrom(d => d.Users));
            cfg.CreateMap<EmployeeDTO, Employee>()
                .ConstructUsing(src => new Employee(src.Username,
                    new Password(src.Password), src.Pesel, src.Id, src.Name, src.LastName,
                    src.Role));
            cfg.CreateMap<DoctorPrivilegesDTO, DoctorPrivileges>()
                .ForMember(x => x.Pwz, s => s.MapFrom(d => new Pwz(d.Pwz)))
                .ForMember(x => x.DoctorSpecialization, s => s.MapFrom(d => d.DoctorSpecialization));
        });
    }

    private MapperConfiguration MapperConfiguration()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Shift, ShiftDTO>()
                .ForMember(x => x.Date, s => s.MapFrom(d => d.Date))
                .ForMember(x => x.Users, s => s.MapFrom(d => d.Users));
            cfg.CreateMap<Employee, EmployeeDTO>()
                .ForMember(x => x.Name, s => s.MapFrom(d => d.Name.Value))
                .ForMember(x => x.Password, s => s.MapFrom(d => d.Password.Value))
                .ForMember(x => x.Pesel, s => s.MapFrom(d => d.Pesel.Value))
                .ForMember(x => x.Username, s => s.MapFrom(d => d.Username.Value))
                .ForMember(x => x.LastName, s => s.MapFrom(d => d.LastName.Value))
                .ForMember(x => x.Role, s => s.MapFrom(d => d.Role))
                .ForMember(x => x.DoctorPrivileges, s => s.MapFrom(d => d.DoctorPrivileges));
            cfg.CreateMap<DoctorPrivileges, DoctorPrivilegesDTO>()
                .ForMember(x => x.Pwz, s => s.MapFrom(d => d.Pwz.Value))
                .ForMember(x => x.DoctorSpecialization, s => s.MapFrom(d => d.DoctorSpecialization));
        });
    }
}
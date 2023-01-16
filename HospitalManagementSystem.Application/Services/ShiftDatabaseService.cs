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
        
        _xmlShiftService = new XMLShiftService(this, "employees.xml", "Employees", mapperConfigurationForDTO,
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

        if (shift is not null)
        {
            return shift;
        }

        return null;
    }
    
    private bool CheckIfEmployeeHasShiftOneDayForward(DateTime date, Employee employee)
    {
        var shiftExist = Items.Exists(x => x.Date.Date == date.AddDays(1).Date && x.Users.Contains(employee));
        return shiftExist;
    }

    private bool CheckIfEmployeeHasShiftOneDayBehind(DateTime date, Employee employee)
    {
        var shiftExist = Items.Exists(x => x.Date.Date == date.AddDays(-1).Date && x.Users.Contains(employee));
        return shiftExist;
    }

    private bool CheckIfEmployeeHasLessThanTenShiftsInMonth(Employee employee)
    {
        var count = Items
            .Where(x => x.Date.Month == DateTime.Now.Month)
            .Count(x => x.Users.Contains(employee));

        if (count <= 10)
        {
            return true;
        }

        return false;
    }

    private bool CheckIfDateIsBeforeActuallDate(DateTime date)
    {
        if(date.Date >= DateTime.Now.Date)
        {
            return true;
        }

        return false;
    }

    private DateTime ParseDate(string text)
    {
        DateTime date;

        try
        {
            if (!DateTime.TryParse(text, out date))
            {
                throw new Exception($"Cannot parse {text} to date.");
            }

            return date;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return default;
    }
    
    private MapperConfiguration MapperConfigurationFromDTO()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ShiftDTO, Shift>()
                .ForMember(x => x.Date, s => s.MapFrom(d => d.Date))
                .ForMember(x => x.Users, s => s.MapFrom(d => d.Users));
            cfg.CreateMap<EmployeeDTO, Employee>()
                .ConstructUsing((EmployeeDTO src) => new Employee(src.Username,
                    new HospitalManagementSystemPassword(src.Password), src.Pesel, src.Id, src.Name, src.LastName, src.Role));
            cfg.CreateMap<DoctorPrivilegesDTO, DoctorPrivileges>()
                .ForMember(x => x.Pwz, s => s.MapFrom(d => new HospitalManagementSystemPWZ(d.Pwz)))
                .ForMember(x => x.Specjalizacja, s => s.MapFrom(d => d.Specjalizacja));
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
                .ForMember(x => x.Role, s => s.MapFrom(d => d.Rola))
                .ForMember(x => x.DoctorPrivileges, s => s.MapFrom(d => d.DoctorPrivileges));
            cfg.CreateMap<DoctorPrivileges, DoctorPrivilegesDTO>()
                .ForMember(x => x.Pwz, s => s.MapFrom(d => d.Pwz.Value))
                .ForMember(x => x.Specjalizacja, s => s.MapFrom(d => d.Specjalizacja));
        });
    }
}
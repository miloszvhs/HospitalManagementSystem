using System.Xml.Serialization;
using AutoMapper;
using HospitalManagementSystem.Application.DTOModels;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

namespace HospitalManagementSystem.Application.Services;

public class XMLService
{
    private readonly IDatabaseService _database;
    private readonly MapperConfiguration _mapperConfiguration;
    private readonly MapperConfiguration _mapperConfigurationDTO;

    private string path { get; }
    private string elementName { get; }
    
    public XMLService(IDatabaseService database, string path, string elementName,
        MapperConfiguration mapperConfiguration,
        MapperConfiguration mapperConfigurationDTO)
    {
        this.path = path;
        this.elementName = elementName;
        _database = database;
        _mapperConfiguration = mapperConfiguration;
        _mapperConfigurationDTO = mapperConfigurationDTO;
    }

    public void RestoreFromXmlFile()
    {
        if (File.Exists(path))
        {
            var xml = File.ReadAllText(path);

            StringReader sr = new(xml);

            XmlRootAttribute root = new();
            root.ElementName = elementName;
            root.IsNullable = true;

            XmlSerializer serializer = new(typeof(List<EmployeeDTO>), root);

            var xmlUsersDTO = (List<EmployeeDTO>)serializer.Deserialize(sr);

            var mapper = new Mapper(_mapperConfiguration);
            var xmlUsers = mapper.Map<List<Employee>>(xmlUsersDTO);

            var employees = _database.Users;
            employees = new List<Employee>(xmlUsers);
        }
        else
        {
            Console.WriteLine($"Nie można pobrać danych z pliku.\nBrak pliku {elementName}.xml");
            _database.Seed();
            Console.WriteLine($"W tym przypadku stworzono nowy plik {elementName}.xml");
        }
    }

    /// <param name="useUsersFromSeed">If file doesn't exist, then create a new one with default seed</param>
    public void SaveToXmlFile()
    {
        var employees = _database.Users;
        var mapper = new Mapper(_mapperConfigurationDTO);
        var employeesDTO = mapper.Map<List<EmployeeDTO>>(employees);
        
        XmlRootAttribute root = new();
        root.ElementName = elementName;
        root.IsNullable = true;
        XmlSerializer serializer = new(typeof(List<EmployeeDTO>), root);

        using StreamWriter sw = new(path);
        serializer.Serialize(sw, employeesDTO);
    }
}
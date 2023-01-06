using System.Xml.Serialization;
using AutoMapper;
using HospitalManagementSystem.Application.DTOModels;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

namespace HospitalManagementSystem.Application.Services;

public class XMLService
{
    private readonly IDatabaseService _database;
    private readonly MapperConfiguration _mapperConfigurationForDTO;
    private readonly MapperConfiguration _mapperConfiguration;

    private string path { get; }
    private string elementName { get; }
    
    public XMLService(IDatabaseService database, string path, string elementName,
        MapperConfiguration mapperConfigurationForDTO,
        MapperConfiguration mapperConfiguration)
    {
        this.path = path;
        this.elementName = elementName;
        _database = database;
        _mapperConfigurationForDTO = mapperConfigurationForDTO;
        _mapperConfiguration = mapperConfiguration;
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

            var mapper = new Mapper(_mapperConfigurationForDTO);
            var xmlUsers = mapper.Map<List<Employee>>(xmlUsersDTO);

            _database.Users = new List<Employee>(xmlUsers);
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
        var mapper = new Mapper(_mapperConfiguration);
        var employeesDTO = mapper.Map<List<EmployeeDTO>>(employees);
        
        XmlRootAttribute root = new();
        root.ElementName = elementName;
        root.IsNullable = true;
        XmlSerializer serializer = new(typeof(List<EmployeeDTO>), root);

        using StreamWriter sw = new(path);
        serializer.Serialize(sw, employeesDTO);
    }
}
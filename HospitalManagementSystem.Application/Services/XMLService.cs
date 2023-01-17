using System.Xml;
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
    private readonly MapperConfiguration _mapperConfigurationForDTO;

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

    private string path { get; }
    private string elementName { get; }

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

            _database.Items = new List<Employee>(xmlUsers);
        }
        else
        {
            Console.WriteLine($"Nie można pobrać danych z pliku.\nBrak pliku {elementName}.xml");
            _database.Seed();
            Console.WriteLine($"W tym przypadku stworzono nowy plik {elementName}.xml");
        }
    }

    public void SaveToXmlFile()
    {
        var employees = _database.Items;
        var mapper = new Mapper(_mapperConfiguration);
        var employeesDTO = mapper.Map<List<EmployeeDTO>>(employees);

        XmlRootAttribute root = new();
        root.ElementName = elementName;
        root.IsNullable = true;
        XmlSerializer serializer = new(typeof(List<EmployeeDTO>), root);

        using StreamWriter sw = new(path);
        using var xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings { Indent = true });
        serializer.Serialize(xmlWriter, employeesDTO);
    }
}
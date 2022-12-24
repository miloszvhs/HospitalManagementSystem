using System.Xml.Serialization;
using AutoMapper;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Application.Services;

public class XMLService<T, V> where T : BaseEntity
{
    private readonly HospitalManagementSystemBaseDb<T> _database;
    private readonly MapperConfiguration _mapperConfiguration;
    private readonly MapperConfiguration _mapperConfigurationDTO;

    public XMLService(HospitalManagementSystemBaseDb<T> database, string path, string elementName,
        MapperConfiguration mapperConfiguration,
        MapperConfiguration mapperConfigurationDTO)
    {
        _path = path;
        _elementName = elementName;
        _database = database;
        _mapperConfiguration = mapperConfiguration;
        _mapperConfigurationDTO = mapperConfigurationDTO;
    }

    private string _path { get; }
    private string _elementName { get; }

    public void RestoreFromXmlFile()
    {
        if (File.Exists(_path))
        {
            var xml = File.ReadAllText(_path);

            StringReader sr = new(xml);

            XmlRootAttribute root = new();
            root.ElementName = _elementName;
            root.IsNullable = true;

            XmlSerializer serializer = new(typeof(List<V>), root);

            var xmlUsersDTO = (List<V>)serializer.Deserialize(sr);

            var mapper = new Mapper(_mapperConfiguration);
            var xmlUsers = mapper.Map<List<T>>(xmlUsersDTO);

            _database.Users = new List<T>(xmlUsers);
        }
        else
        {
            Console.WriteLine($"Nie można pobrać danych z pliku.\nBrak pliku {_elementName}.xml");
        }
    }

    public void SaveToXmlFile()
    {
        var mapper = new Mapper(_mapperConfigurationDTO);
        var usersDTO = mapper.Map<List<V>>(_database.Users);

        XmlRootAttribute root = new();
        root.ElementName = _elementName;
        root.IsNullable = true;
        XmlSerializer serializer = new(typeof(List<V>), root);

        using StreamWriter sw = new(_path);
        serializer.Serialize(sw, usersDTO);
    }
}
using System.Xml.Serialization;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Application.Services;

public class XMLService<T> where T : BaseEntity
{
    private string _path { get; }
    private string _elementName { get; }
    private readonly HospitalManagementSystemBaseDb<T> _database;

    public XMLService(HospitalManagementSystemBaseDb<T> database, string path, string elementName)
    {
        _path = path;
        _elementName = elementName;
        _database = database;
    }
    
    public void RestoreFromXmlFile()
    {
        if(File.Exists(_path))
        {
            var xml = File.ReadAllText(_path);

            StringReader sr = new(xml);

            XmlRootAttribute root = new();
            root.ElementName = _elementName;
            root.IsNullable = true;

            XmlSerializer serializer = new(typeof(List<T>), root);

            var xmlUsers = (List<T>)serializer.Deserialize(sr);

            _database.Users = new List<T>(xmlUsers);
        }
    }
    
    public void SaveToXmlFile()
    {
        XmlRootAttribute root = new();
        root.ElementName = _elementName;
        root.IsNullable = true;
        XmlSerializer serializer = new(typeof(List<T>), root);

        using StreamWriter sw = new(_path);
        serializer.Serialize(sw, _database.Users);
    }
}
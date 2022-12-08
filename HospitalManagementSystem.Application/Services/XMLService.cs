using System.Xml.Serialization;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Application.Services;

public class XMLService<T> where T : BaseEntity
{
    public string Path { get; }
    private readonly HospitalManagementSystemBaseDb<T> _database;

    public XMLService(HospitalManagementSystemBaseDb<T> database, string path)
    {
        Path = path;
        _database = database;
    }
    
    public void RestoreFromXMLFile()
    {
        if(File.Exists(Path))
        {
            var xml = File.ReadAllText(Path);

            StringReader sr = new(xml);

            XmlRootAttribute root = new();
            root.ElementName = "Employees";
            root.IsNullable = true;

            XmlSerializer serializer = new(typeof(List<Employee>), root);

            var xmlUsers = (List<T>)serializer.Deserialize(sr);

            _database.Users = new List<T>(xmlUsers);
        }
    }
    
    public void SaveToXMLFile()
    {
        XmlRootAttribute root = new();
        root.ElementName = "Employees";
        root.IsNullable = true;
        XmlSerializer serializer = new(typeof(List<Employee>), root);

        using StreamWriter sw = new(Path);
        serializer.Serialize(sw, _database.Users);
    }
}
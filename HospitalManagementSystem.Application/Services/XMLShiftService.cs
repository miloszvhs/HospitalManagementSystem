using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using HospitalManagementSystem.Application.DTOModels;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Interfaces;

namespace HospitalManagementSystem.Application.Services;

public class XMLShiftService
{
    private readonly IShiftDatabaseService _database;
    private readonly MapperConfiguration _mapperConfiguration;
    private readonly MapperConfiguration _mapperConfigurationForDTO;

    public XMLShiftService(IShiftDatabaseService database, string path, string elementName,
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

            XmlSerializer serializer = new(typeof(List<ShiftDTO>), root);

            var xmlShiftsDTO = (List<ShiftDTO>)serializer.Deserialize(sr);

            var mapper = new Mapper(_mapperConfigurationForDTO);
            var xmlShifts = mapper.Map<List<Shift>>(xmlShiftsDTO);

            _database.Items = new List<Shift>(xmlShifts);
        }
        else
        {
            Console.WriteLine($"Nie można pobrać danych z pliku.\nBrak pliku {elementName}.xml");
        }
    }

    public void SaveToXmlFile()
    {
        var shifts = _database.Items;
        var mapper = new Mapper(_mapperConfiguration);
        var shiftsDTO = mapper.Map<List<ShiftDTO>>(shifts);

        XmlRootAttribute root = new();
        root.ElementName = elementName;
        root.IsNullable = true;
        XmlSerializer serializer = new(typeof(List<ShiftDTO>), root);

        using StreamWriter sw = new(path);
        using var xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings { Indent = true });
        serializer.Serialize(xmlWriter, shiftsDTO);
    }
}
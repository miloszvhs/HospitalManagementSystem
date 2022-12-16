using AutoMapper;
using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Application.DTOModels;

public class DoctorDTO 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public byte[] Password { get; set; }
    public string Pwz { get; set; }
    public int Specjalizacja { get; set; }
}
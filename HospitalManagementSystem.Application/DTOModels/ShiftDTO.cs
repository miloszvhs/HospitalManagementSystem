﻿using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Application.DTOModels;

public class ShiftDTO
{
    public DateOnly Date { get; set; }
    public List<Employee> Users { get; set; }
}
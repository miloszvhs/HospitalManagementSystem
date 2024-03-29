﻿using HospitalManagementSystem.Shared.Abstractions;

namespace HospitalManagementSystem.Domain.Exceptions;

public class InvalidPasswordException : CustomException
{
    public InvalidPasswordException() : base($"Invalid password")
    {
    }
}
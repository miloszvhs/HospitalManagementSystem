using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Exceptions;
using HospitalManagementSystem.Domain.ValueObjects;
using Shouldly;
using Xunit;
using Xunit.Sdk;

namespace HospitalManagementSystem.Test.Unit.Entities;

public class ShiftTest
{
    [Fact]
    public void given_employee_for_already_existing_employee_should_fail()
    {
        _shift.AddEmployee(_employee);
        
        var exception = Record.Exception(() => _shift.AddEmployee(_employee));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserAlreadyExistsException>();
    }
    
    [Fact]
    public void given_employee_with_the_same_specialization_should_fail()
    {
        var employeeWithTheSameSpecialization = new Employee(new Username("Test2"),
            new Password(new byte[] { 0, 1, 2 }),
            new Pesel("11111111112"),
            new Id(2),
            new Name("Test2"),
            new Name("Test2"),
            Role.Lekarz,
            new DoctorPrivileges(new Pwz("6130102"), DoctorSpecialization.Kardiolog));
        _shift.AddEmployee(_employee);
        
        var exception = Record.Exception(() => _shift.AddEmployee(employeeWithTheSameSpecialization));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserWithTheSameSpecializationAlreadyExistsException>();
    }

    #region Arrange

    private readonly Shift _shift;
    private readonly Employee _employee;
    
    public ShiftTest()
    {
        _employee = new Employee(new Username("Test"),
            new Password(new byte[] { 0, 1, 2 }),
            new Pesel("11111111111"),
            new Id(1),
            new Name("Test"),
            new Name("Test"),
            Role.Lekarz,
            new DoctorPrivileges(new Pwz("6130101"), DoctorSpecialization.Kardiolog));
        _shift = new Shift(new DateTime(2023, 01, 30));
    }

    #endregion
 
}
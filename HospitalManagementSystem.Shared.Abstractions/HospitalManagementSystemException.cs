namespace HospitalManagementSystem.Shared.Abstractions;

public abstract class HospitalManagementSystemException : Exception
{
    protected HospitalManagementSystemException(string message) : base(message)
    {
    }
}
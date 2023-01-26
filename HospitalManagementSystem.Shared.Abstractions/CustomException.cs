namespace HospitalManagementSystem.Shared.Abstractions;

public abstract class CustomException : Exception
{
    protected CustomException(string message) : base(message)
    {
    }
}
namespace HospitalManagementSystem.Shared.Abstractions.Helpers;

public static class Helper
{
    public static int CheckStringAndConvertToInt(string text)
    {
        var result = 0;

        try
        {
            if (!int.TryParse(text, out result)) throw new Exception($"Cannot parse {text} to int.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return result;
    }
    
    public static DateTime ParseDate(string text)
    {
        DateTime date;

        try
        {
            if (!DateTime.TryParse(text, out date)) throw new Exception($"Cannot parse {text} to date.");

            return date;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return default;
    }
}
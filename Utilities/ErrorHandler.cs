using HavenHotel.Interfaces;

namespace HavenHotel.Utilities;

public class ErrorHandler : IErrorHandler
{
    public void DisplayError(string message)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
        Console.Write("Press any key to return...");
        Console.ReadKey();
    }
}

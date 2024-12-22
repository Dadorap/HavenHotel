using HavenHotel.Interfaces;

namespace HavenHotel.Utilities;

public class Header : IHeader
{
    public void DisplayHeader()
    {
        string haven = @"
    ██╗  ██╗ █████╗ ██╗   ██╗███████╗███╗   ██╗
    ██║  ██║██╔══██╗██║   ██║██╔════╝████╗  ██║
    ███████║███████║██║   ██║█████╗  ██╔██╗ ██║
    ██╔══██║██╔══██║██║   ██║██╔══╝  ██║╚██╗██║
    ██║  ██║██║  ██║╚██████╔╝███████╗██║ ╚████║
    ╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚═╝  ╚═══╝
    ██╗  ██╗ ██████╗ ████████╗███████╗██╗     
    ██║  ██║██╔═══██╗╚══██╔══╝██╔════╝██║     
    ███████║██║   ██║   ██║   █████╗  ██║     
    ██╔══██║██║   ██║   ██║   ██╔══╝  ██║     
    ██║  ██║╚██████╔╝   ██║   ███████╗███████╗
    ╚═╝  ╚═╝ ╚═════╝    ╚═╝   ╚══════╝╚══════╝

";

        string[] havenLines = haven.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        foreach (string line in havenLines)
        {
            int padding = (Console.WindowWidth - line.Length) / 2;
            Console.SetCursorPosition(Math.Max(padding, 0), Console.CursorTop);
            Console.WriteLine(line);
        }
        Console.ResetColor();
    }

}

using HavenHotel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Utilities
{
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
}

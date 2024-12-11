using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HavenHotel.Interfaces;

namespace HavenHotel.Utilities
{
    public class Exit : IExit
    {

        public void ExitConsole()
        {
            Console.Clear();
            var currentTime = DateTime.Now;
            var time = currentTime.ToString("HH");
            var dayTime =
                currentTime.Hour >= 6 && currentTime.Hour < 18 ?
                "day" : "evening";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Have a nice {dayTime}!");
            Console.ResetColor();
            Environment.Exit(0);
        }
    }
}

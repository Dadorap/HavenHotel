using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HavenHotel.Interfaces;

namespace HavenHotel.Common
{
    public class UserMessages : IUserMessages
    {
        public void ShowCancelMessage()
        {
            Console.ResetColor();
            Console.Write("Type ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("'cancel' ");
            Console.ResetColor();
            Console.WriteLine("to return to main menu.");
        }

    }
}

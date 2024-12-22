using HavenHotel.Interfaces;

namespace HavenHotel.Utilities
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

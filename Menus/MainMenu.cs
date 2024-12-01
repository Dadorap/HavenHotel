using HavenHotel.InterfaceFolder;
using HavenHotel.Interfaces;


namespace HavenHotel.MenuFolder
{
    public class MainMenu : IMainMenu
    {
        private readonly IHeader _header;
        private readonly IExit _exit;

        public MainMenu(IHeader header, IExit exit)
        {
            _header = header;
            _exit = exit;
        }
        public void DisplayMenu()
        {
            int currentSelect = 0;
            List<string> menu = new()
    {
        "Rooms",
        "Guests",
        "Bookings",
        "Exit"
    };

            while (true)
            {
                Console.Clear();
                _header.DisplayHeader();

                int consoleHeight = Console.WindowHeight;
                int consoleWidth = Console.WindowWidth;
                int headerHeight = 3;
                int verticalOffset = (consoleHeight - menu.Count) / 2 + headerHeight;

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                string instruction = "Main Menu";
                int instructionX = (consoleWidth - instruction.Length) / 2;
                Console.SetCursorPosition(instructionX, verticalOffset - 2);
                Console.Write(instruction);
                Console.ResetColor();

                for (int i = 0; i < menu.Count; i++)
                {
                    string menuItem = menu[i];
                    int menuItemX = (consoleWidth - menuItem.Length) / 2;

                    Console.SetCursorPosition(menuItemX, verticalOffset + i);

                    if (i == currentSelect)
                    {
                        if (menu[i].ToLower() == "exit")
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        }
                        Console.WriteLine(menuItem);
                    }
                    else
                    {
                        Console.WriteLine(menuItem);
                    }
                    Console.ResetColor();
                }

                ConsoleKey keyPressed = Console.ReadKey(true).Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    currentSelect = currentSelect > 0 ? currentSelect - 1 : menu.Count - 1;
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    currentSelect = currentSelect < menu.Count - 1 ? currentSelect + 1 : 0;
                }
                else if (keyPressed == ConsoleKey.Enter)
                {
                    switch (currentSelect)
                    {
                        case 0:
                          
                            break;
                        case 1:
                           
                            break;
                        case 2:
                            break;
                        case 3:
                            _exit.ExitConsole();
                            return;
                    }

                }
            }
        }

    }
}

using HavenHotel.Interfaces;

namespace HavenHotel.Utilities;

public class Menu : ISharedMenu
{
    private readonly IHeader _header;

    public Menu(IHeader header, IExit exit)
    {
        _header = header;

    }

    public void DisplayMenu(string display, List<string> menu, Action option1 = null,
        Action option2 = null, Action option3 = null, Action option4 = null,
        Action option5 = null, Action option6 = null, Action option7 = null)
    {
        int currentSelect = 0;


        while (true)
        {
            Console.Clear();

            _header.DisplayHeader();
            int headerHeight = Console.CursorTop;
            int verticalOffset = headerHeight + 2;

            int consoleHeight = Console.WindowHeight;
            int consoleWidth = Console.WindowWidth;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string instruction = display.ToUpper();
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
                        option1();
                        break;
                    case 1:
                        option2();
                        break;
                    case 2:
                        option3();
                        break;
                    case 3:
                        option4();
                        break;
                    case 4:
                        option5();
                        break;
                    case 5:
                        option6();
                        break;
                    case 6:
                        option7();
                        return;
                }

            }
        }
    }

}

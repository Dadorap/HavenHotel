using HavenHotel.InterfaceFolder;
using HavenHotel.Interfaces;

namespace HavenHotel.MenuFolder
{
    public class Menu : IMenu
    {
        private readonly IHeader _header;

        public Menu(IHeader header, IExit exit)
        {
            _header = header;

        }

        public void DisplayMenu(List<string> menu, Action option1, Action option2, Action option3, Action option4, Action option5, Action option6)
        {
            int currentSelect = 0;


            while (true)
            {
                Console.Clear();


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
                          
                            return;
                    }

                }
            }
        }

    }
}

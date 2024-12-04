using Autofac.Features.AttributeFilters;
using HavenHotel.Common;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Rooms.Services.Display
{
    public class DisplayRoomDetails : IDisplay
    {
        private readonly IRepository<Room> _roomRepo;
        private readonly IDisplayRight _displayRight;
        private readonly IErrorHandler _errorHandler;
        private readonly IUserMessages _userMessages;
        private readonly INavigationHelper _navigationHelper;
        public DisplayRoomDetails(IRepository<Room> roomRepo,
            [KeyFilter("DisplayGuestsRight")] IDisplayRight displayRight,
            IErrorHandler errorHandler,
            IUserMessages userMessages,
            INavigationHelper navigationHelper)
        {
            _roomRepo = roomRepo;
            _displayRight = displayRight;
            _errorHandler = errorHandler;
            _userMessages = userMessages;
            _navigationHelper = navigationHelper;
        }
        public void DisplayById()
        {
            int invalidCounter = 0;
            while (true)
            {
                try
                {
                    Console.Clear();
                    _displayRight.DisplayRightAligned("room");

                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("===== DISPLAY A ROOM =====");
                    _userMessages.ShowCancelMessage();
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write("Please enter the Room's ID: ");
                    string idInput = Console.ReadLine();
                    _navigationHelper.ReturnToMenu(idInput);
                    if (int.TryParse(idInput, out int id))
                    {
                        var room = _roomRepo.GetItemById(id);
                        var roomAvailability = room.IsActive ? "available" : "occupied";

                        Console.Clear();
                        Console.WriteLine("╔════════════╦═════════════╦════════════╦══════════════╦═════════════╗");
                        Console.WriteLine("║ Room Type  ║ Room Size   ║ Extra Beds ║ Total Guests ║ Availability║");
                        Console.WriteLine("╠════════════╬═════════════╬════════════╬══════════════╣═════════════╣");
                        Console.ForegroundColor = ConsoleColor.Gray;

                        Console.WriteLine($"║ {room.RoomType,-10} ║ {room.Size + "m²",-11} ║ {room.ExtraBed,-10} ║ {room.TotalGuests,-12} ║ {roomAvailability,-11} ║");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;

                        Console.WriteLine("╚════════════╩═════════════╩════════════╩══════════════╩═════════════╝");
                        Console.ResetColor();

                        Console.Write("Press any key to return to menu...");
                        Console.ReadKey();
                        return;

                    }
                    else if (invalidCounter >= 2)
                    {
                        _errorHandler.DisplayError("List's on the right ->. " +
                            "\nChoose an ID from the list please:)");
                        invalidCounter = 0;
                    }
                    else
                    {
                        invalidCounter++;
                        _errorHandler.DisplayError("Invalid id input, try again...");
                    }


                }
                catch (Exception)
                {
                    invalidCounter++;
                    _errorHandler.DisplayError("Invalid id number input, try again...");
                }
            }
        }
    }
}

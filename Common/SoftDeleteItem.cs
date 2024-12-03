using Autofac.Features.AttributeFilters;
using HavenHotel.Bookings;
using HavenHotel.Guests;
using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Common
{
    public class SoftDeleteItem : ISoftDeleteItem
    {


        private readonly IRepository<Booking> _bookingRepo;
        private readonly IDisplayRight _displayRight;
        private readonly IErrorHandler _errorHandler;
        private readonly IUserMessages _userMessages;
        private readonly INavigationHelper _navigationHelper;
        private readonly IRepository<Room> _roomsRepo;
        private readonly IRepository<Guest> _guestsRepo;
        public SoftDeleteItem
            (
            IRepository<Booking> bookingRepo,
            IRepository<Room> roomsRepo,
            IRepository<Guest> guestRepo,
            [KeyFilter("DisplayBookingsIDRight")] IDisplayRight displayRight,
            IErrorHandler errorHandler,
            IUserMessages userMessages,
            INavigationHelper navigationHelper
            )
        {
            _bookingRepo = bookingRepo;
            _roomsRepo = roomsRepo;
            _guestsRepo = guestRepo;
            _displayRight = displayRight;
            _errorHandler = errorHandler;
            _userMessages = userMessages;
            _navigationHelper = navigationHelper;
        }


        public void SoftDelete(string text)
        {
            var textDisplay = text.ToUpper();

            while (true)
            {
                try
                {
                    Console.Clear();
                    _displayRight.DisplayRightAligned(textDisplay);

                    // Fetch the appropriate repository count for validation
                    int itemsLength = textDisplay switch
                    {
                        "BOOKING" => _bookingRepo.GetAllItems().Count(),
                        "ROOM" => _roomsRepo.GetAllItems().Count(),
                        "GUEST" => _guestsRepo.GetAllItems().Count(),
                        _ => throw new ArgumentException($"Invalid input: {text}")
                    };

                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"===== SOFT DELETE A {textDisplay} =====");
                    _userMessages.ShowCancelMessage();
                    Console.ResetColor();

                    Console.Write("Please enter the ID: ");
                    string idInput = Console.ReadLine();
                    _navigationHelper.ReturnToMenu(idInput);

                    if (int.TryParse(idInput, out int id) && id >= 0 && id < itemsLength)
                    {
                        dynamic item = textDisplay switch
                        {
                            "BOOKING" => _bookingRepo.GetItemById(id),
                            "ROOM" => _roomsRepo.GetItemById(id),
                            "GUEST" => _guestsRepo.GetItemById(id),
                            _ => throw new ArgumentException($"Invalid input: {text}")
                        };

                        item.IsActive = false;

                        switch (textDisplay)
                        {
                            case "BOOKING":
                                _bookingRepo.SaveChanges();
                                break;
                            case "ROOM":
                                _roomsRepo.SaveChanges();
                                break;
                            case "GUEST":
                                _guestsRepo.SaveChanges();
                                break;
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Successfully soft-deleted the {textDisplay} with ID {id}.");
                        Console.ResetColor();
                        Console.Write("Press any key to return to the menu...");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {

                        _errorHandler.DisplayError("Invalid ID. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    _errorHandler.DisplayError($"An error occurred: {ex.Message}");
                }
            }
        }

    }
}




﻿using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Common;

public class HardDeleteItem : IHardDeleteItem
{

    private readonly IRepository<Booking> _bookingRepo;
    private readonly IDisplayRight _displayRight;
    private readonly IErrorHandler _errorHandler;
    private readonly IUserMessages _userMessages;
    private readonly INavigationHelper _navigationHelper;
    private readonly IRepository<Room> _roomsRepo;
    private readonly IRepository<Guest> _guestsRepo;
    public HardDeleteItem
        (
        IRepository<Booking> bookingRepo,
        IRepository<Room> roomsRepo,
        IRepository<Guest> guestRepo,
        IDisplayRight displayRight,
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


    public void HardDelete(string text)
    {
        var textDisplay = text.ToUpper();

        while (true)
        {
            try
            {
                Console.Clear();
                _displayRight.DisplayRightAligned(textDisplay);

                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"===== Hard DELETE A {textDisplay} =====");
                _userMessages.ShowCancelMessage();
                Console.ResetColor();

                Console.Write("Please enter the ID: ");
                string idInput = Console.ReadLine();
                _navigationHelper.ReturnToMenu(idInput);

                if (!int.TryParse(idInput, out int id))
                {
                    _errorHandler.DisplayError("Invalid ID. Please try again.");
                    continue;
                }


                switch (textDisplay)
                {
                    case "BOOKING":
                        _bookingRepo.RemoveItemById(id);
                        break;
                    case "ROOM":
                        _roomsRepo.RemoveItemById(id);
                        break;
                    case "GUEST":
                        _guestsRepo.RemoveItemById(id);
                        break;
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Successfully soft-deleted the {textDisplay} with ID {id}.");
                Console.ResetColor();
                Console.Write("Press any key to return to the menu...");
                Console.ReadKey();
                return;




            }
            catch (Exception ex)
            {
                _errorHandler.DisplayError($"An error occurred: {ex.Message}");
            }
        }
    }


}

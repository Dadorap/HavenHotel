﻿using Autofac.Features.AttributeFilters;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Common;

public class UnDeleteItem : IUnDeleteItem
{


    private readonly IRepository<Booking> _bookingRepo;
    private readonly IDisplayRight _displayRight;
    private readonly IErrorHandler _errorHandler;
    private readonly IUserMessages _userMessages;
    private readonly INavigationHelper _navigationHelper;
    private readonly IRepository<Room> _roomsRepo;
    private readonly IRepository<Guest> _guestsRepo;
    public UnDeleteItem
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


    public void UnDelete(string text)
    {
        var textDisplay = text.ToUpper();

        while (true)
        {
            try
            {
                Console.Clear();
                _displayRight.DisplayRightAligned(textDisplay, "false");

                int itemsLength = textDisplay switch
                {
                    "BOOKING" => _bookingRepo.GetAllItems().Count(),
                    "ROOM" => _roomsRepo.GetAllItems().Count(),
                    "GUEST" => _guestsRepo.GetAllItems().Count(),
                    _ => throw new ArgumentException($"Invalid input: {text}")
                };

                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"== REVERSING A DELETION ON: {textDisplay} ==");
                _userMessages.ShowCancelMessage();
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Please enter the ID-number: ");

                string idInput = Console.ReadLine();
                _navigationHelper.ReturnToMenu(idInput);

                if (int.TryParse(idInput, out int id))
                {
                    dynamic item = textDisplay switch
                    {
                        "BOOKING" => _bookingRepo.GetItemById(id),
                        "ROOM" => _roomsRepo.GetItemById(id),
                        "GUEST" => _guestsRepo.GetItemById(id),
                        _ => throw new ArgumentException($"Invalid input: {text}")
                    };

                    if (!item.IsActive)
                    {
                        item.IsActive = true;

                    }

                    else
                    {
                        _errorHandler.DisplayError("ID is already active. Please try again.");
                        continue;
                    }

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

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Successfully undeleted the {textDisplay} with ID: {id}.");
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
            catch (Exception)
            {
                _errorHandler.DisplayError("Invalid ID. Please try again.");
            }
        }
    }

}




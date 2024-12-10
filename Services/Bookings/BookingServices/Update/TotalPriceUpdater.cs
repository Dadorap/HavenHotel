using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Services.BookingServices.Services.Update;

public class TotalPriceUpdater : ITotalPriceUpdater
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Room> _roomRepo;
    private readonly Lazy<INavigationHelper> _navigationHelper;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IErrorHandler _errorHandler;
    private readonly IUserMessages _userMessages;
    private readonly IPromptForBookingId _promptForBookingId;


    public TotalPriceUpdater(
        IRepository<Room> roomRepo,
        IRepository<Booking> bookingRepo,
        Lazy<INavigationHelper> navigationHelper,
        [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
        IErrorHandler errorHandler,
        IUserMessages userMessages,
        IPromptForBookingId promptForBookingId

        )
    {

        _roomRepo = roomRepo;
        _bookingRepo = bookingRepo;
        _navigationHelper = navigationHelper;
        _errorHandler = errorHandler;
        _userMessages = userMessages;
        _mainMenu = mainMenu;
        _promptForBookingId = promptForBookingId;
    }
    public void UpdateTotalPrice()
    {
        while (true)
        {
            try
            {
                var id = _promptForBookingId.GetValidBookingId("Recalculate Total Price", "totalPrice");

                var bookingPrice = _bookingRepo.GetItemById(id).TotalPrice;
                if (bookingPrice == null)
                {
                    _errorHandler.DisplayError("Booking not found. Try again...");
                    continue;
                }
                Console.WriteLine($"{bookingPrice:C}. Will be changed.");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter new total price: ");
                string totalP = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(totalP);

                if (!int.TryParse(totalP, out int totalPrice))
                {
                    _errorHandler.DisplayError("Invalid price input try again...");
                    continue;
                }

                bookingPrice = totalPrice;
                _bookingRepo.SaveChanges();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The new total price is set at {totalP:C}");
                Console.ResetColor();
                Console.Write("Press any key to return to menu...");
                Console.ReadKey();
                _mainMenu.Value.DisplayMenu();
                break;

            }
            catch (Exception)
            {
                _errorHandler.DisplayError("Invalid price input try again...");
                continue;
            }
        }
    }
}

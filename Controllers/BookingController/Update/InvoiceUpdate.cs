using Autofac.Features.AttributeFilters;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Controllers.BookingController.Update;

public class InvoiceUpdate : IInvoiceUpdate
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Room> _roomRepo;
    private readonly Lazy<INavigationHelper> _navigationHelper;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IErrorHandler _errorHandler;
    private readonly IUserMessages _userMessages;
    private readonly IPromptForBookingId _promptForBookingId;


    public InvoiceUpdate(
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
    public void InvoiceUpdater()
    {
        while (true)
        {
            try
            {
                var id = _promptForBookingId.GetValidBookingId("invoice", "totalPrice");

                var booking = _bookingRepo.GetItemById(id);
                if (booking.TotalPrice == null)
                {
                    _errorHandler.DisplayError("Booking not found. Try again...");
                    continue;
                }
                var currentInvoiceStatus = booking.IsPaid ? "paid" : "unpaid";
                Console.WriteLine($"Currently the invoice is: {currentInvoiceStatus}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.Write("Enter 'paid' or 'unpaid' \nto change the invoice status: ");
                string userInput = Console.ReadLine().Trim().ToLower();
                _navigationHelper.Value.ReturnToMenu(userInput);
                if (!(userInput == "paid" || userInput == "unpaid"))
                {
                    _errorHandler.DisplayError("Invalid answer input try again...");
                    continue;
                }
                var isPaid = userInput == "paid";
                booking.IsPaid = isPaid;
                _bookingRepo.Update(booking);
                var invoice = isPaid ? "paid" : "unpaid";

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The invoice is now {invoice}.");
                Console.ResetColor();
                Console.Write("Press any key to return to menu...");
                Console.ReadKey();
                _mainMenu.Value.DisplayMenu();
                break;

            }
            catch (Exception)
            {
                _errorHandler.DisplayError("Invalid input try again...");
                continue;
            }
        }
    }
}

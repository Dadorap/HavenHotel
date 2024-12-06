using Autofac.Features.AttributeFilters;
using HavenHotel.Guests;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Bookings.Services.Update;

public class DateRange : IDateRange
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Room> _roomRepo;
    private readonly Lazy<INavigationHelper> _navigationHelper;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IDateValidator _dateValidator;
    private readonly IErrorHandler _errorHandler;
    private readonly IUserMessages _userMessages;
    private readonly IPromptForBookingId _promptForBookingId;


    public DateRange(
        IRepository<Room> roomRepo,
        IRepository<Booking> bookingRepo,
        Lazy<INavigationHelper> navigationHelper,
        [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
        IDateValidator dateValidator,
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
        _dateValidator = dateValidator;
        _promptForBookingId = promptForBookingId;
    }

    public void UpdateDate()
    {
        while(true)
        {
            try
            {
                var id = _promptForBookingId.GetValidBookingId("update booking date" , "date");
                
                var bookingId = _bookingRepo.GetItemById(id);
                Console.WriteLine($"Current check-in: {bookingId.StartDate}");
                Console.WriteLine("Enter the new check-in date (yyyy-MM-dd): ");
                string atDate = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(atDate);
                if (!DateOnly.TryParse(atDate, out DateOnly startDate) || 
                    !_dateValidator.IsCorrectStartDate(startDate))
                {
                    _errorHandler.DisplayError("Invalid date input try again...");
                    continue;
                }
                Console.WriteLine($"Current check-out date: {bookingId.EndDate}");
                Console.WriteLine("Enter the new checkout date (yyyy-MM-dd)");
                string lastDate = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(lastDate);
                if (!DateOnly.TryParse(lastDate, out DateOnly endDate) || 
                    !_dateValidator.IsCorrectEndDate(startDate, endDate))
                {
                    _errorHandler.DisplayError("Invalid date input try again...");
                    continue;
                }
                var roomPrice = _roomRepo.GetItemById(bookingId.RoomId).Price;
                int totalDays = (endDate.ToDateTime(TimeOnly.MinValue) -
                                startDate.ToDateTime(TimeOnly.MinValue)).Days;
                var daysTotal = totalDays == 0 ? 1 : totalDays;
                var totalPrice = daysTotal * roomPrice;
                var bookings = _bookingRepo.GetAllItems()
                    .Where(b => b.Id == id).ToList();

                bookings.ForEach(b =>
                {
                    b.StartDate = startDate;
                    b.EndDate = endDate;
                    b.TotalPrice = totalPrice;
                    _bookingRepo.Update(b);
                });
                _bookingRepo.SaveChanges();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The new check-in {startDate} and check-out date " +
                    $"{endDate} has been set.\nTotal price: {totalPrice:C}");
                Console.Write("Press any key to return to menu...");
                Console.ReadKey();
                _mainMenu.Value.DisplayMenu();
                Console.ResetColor();
                break;
            }
            catch (Exception ex)
            {
                _errorHandler.DisplayError($"Invalid input. try again...{ex.Message}");

                continue;
            }
        }
    }


}

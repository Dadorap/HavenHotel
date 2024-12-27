using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Controllers.BookingController.Update;

public class DateRangeUpdate : IDateRangeUpdate
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Room> _roomRepo;
    private readonly Lazy<INavigationHelper> _navigationHelper;
    private readonly IDateValidator _dateValidator;
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForBookingId _promptForBookingId;
    private readonly IUpdateConfirmation _updateConfirmation;


    public DateRangeUpdate(
        IRepository<Room> roomRepo,
        IRepository<Booking> bookingRepo,
        Lazy<INavigationHelper> navigationHelper,
        IDateValidator dateValidator,
        IErrorHandler errorHandler,
        IUserMessages userMessages,
        IPromptForBookingId promptForBookingId,
        IUpdateConfirmation updateConfirmation

        )
    {

        _roomRepo = roomRepo;
        _bookingRepo = bookingRepo;
        _navigationHelper = navigationHelper;
        _errorHandler = errorHandler;
        _dateValidator = dateValidator;
        _promptForBookingId = promptForBookingId;
        _updateConfirmation = updateConfirmation;
    }

    public void UpdateDate()
    {
        while (true)
        {
            try
            {
                var id = _promptForBookingId.GetValidBookingId("update booking date", "date");

                var booking = _bookingRepo.GetItemById(id);
                if (booking == null)
                {
                    _errorHandler.DisplayError("Booking not found. Try again...");
                    continue;
                }
                Console.WriteLine($"Current check-in: {booking.StartDate}");
                Console.WriteLine("Enter the new check-in date (yyyy-MM-dd): ");
                string atDate = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(atDate);
                if (!DateOnly.TryParse(atDate, out DateOnly checkInDate) ||
                    !_dateValidator.IsCorrectStartDate(checkInDate))
                {
                    _errorHandler.DisplayError("Invalid date input try again...");
                    continue;
                }

                Console.WriteLine($"Current check-out date: {booking.EndDate}");
                Console.Write("Enter number of nights: ");
                string nightsInput = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(nightsInput);

                if (!int.TryParse(nightsInput, out int nights) || nights <= 0)
                {
                    Console.WriteLine("Invalid number of nights. Please enter a positive number.");
                    return;
                }
                DateOnly checkOutDate = checkInDate.AddDays(nights);

                var roomPrice = _roomRepo.GetItemById(booking.RoomId).Price;
                var totalPrice = nights * roomPrice;
                var bookings = _bookingRepo.GetAllItems()
                    .Where(b => b.Id == id).ToList();

                bookings.ForEach(b =>
                {
                    b.StartDate = checkInDate;
                    b.EndDate = checkOutDate;
                    b.TotalPrice = totalPrice;
                    _bookingRepo.Update(b);
                });
                _bookingRepo.Update(booking);
                _updateConfirmation.Confirmation($"The new check-in {checkInDate} and check-out date " +
                    $"{checkOutDate} has been set.\nTotal price: {totalPrice:C}");
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

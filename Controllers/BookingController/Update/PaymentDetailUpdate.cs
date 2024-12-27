using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Controllers.BookingController.Update;

public class PaymentDetailUpdate : IPaymentDetailUpdate
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Room> _roomRepo;
    private readonly Lazy<INavigationHelper> _navigationHelper;
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForBookingId _promptForBookingId;
    private readonly IUpdateConfirmation _updateConfirmation;



    public PaymentDetailUpdate(
        IRepository<Room> roomRepo,
        IRepository<Booking> bookingRepo,
        Lazy<INavigationHelper> navigationHelper,
        IErrorHandler errorHandler,
        IPromptForBookingId promptForBookingId,
        IUpdateConfirmation updateConfirmation

        )
    {

        _roomRepo = roomRepo;
        _bookingRepo = bookingRepo;
        _navigationHelper = navigationHelper;
        _errorHandler = errorHandler;
        _promptForBookingId = promptForBookingId;
        _updateConfirmation = updateConfirmation;
    }
    public void PaymentDetailUpdater()
    {
        while (true)
        {
            try
            {
                var id = _promptForBookingId.GetValidBookingId("Recalculate Total Price", "totalPrice");

                var booking = _bookingRepo.GetItemById(id);
                if (booking.TotalPrice == null)
                {
                    _errorHandler.DisplayError("Booking not found. Try again...");
                    continue;
                }
                Console.WriteLine($"{booking.TotalPrice:C}. Will be changed.");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter new total price: ");
                string totalP = Console.ReadLine().Trim();
                _navigationHelper.Value.ReturnToMenu(totalP);

                if (!decimal.TryParse(totalP, out decimal totalPrice) 
                    || totalPrice < 0)
                {
                    _errorHandler.DisplayError("Invalid price input try again...");
                    continue;
                }

                booking.TotalPrice = totalPrice;
                _bookingRepo.Update(booking);
                _updateConfirmation.Confirmation($"The new total price is set at {totalP:C}");


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

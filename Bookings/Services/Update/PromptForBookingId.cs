using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;

namespace HavenHotel.Bookings.Services.Update;

public class PromptForBookingId : IPromptForBookingId
{
    private readonly IBookingIdRenderer _bookingIdRenderer;
    private readonly IErrorHandler _errorHandler;
    private readonly Lazy<INavigationHelper> _navigationHelper;

    public PromptForBookingId(
        IBookingIdRenderer bookingIdRenderer,
        IErrorHandler errorHandler,
        Lazy<INavigationHelper> navigationHelper
        )
    {
        _bookingIdRenderer = bookingIdRenderer;
        _errorHandler = errorHandler;
        _navigationHelper = navigationHelper;
    }

    public int GetValidBookingId(string actionDescription)
    {
        while (true)
        {
            Console.Clear();
            _bookingIdRenderer.DisplayBookingNumber(actionDescription);

            Console.Write("Enter booking ID: ");
            string inputId = Console.ReadLine();
            _navigationHelper.Value.ReturnToMenu(inputId);

            if (!int.TryParse(inputId, out int id))
            {
                _errorHandler.DisplayError("Invalid ID input. Try again...");
                continue;
            }

            return id;
        }
    }


}

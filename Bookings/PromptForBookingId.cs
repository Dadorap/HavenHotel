using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HavenHotel.Bookings.Services;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;

namespace HavenHotel.Bookings;

public class PromptForBookingId : IPromptForBookingId
{
    private readonly IBookingIdRenderer _bookingIdRenderer;
    private readonly IErrorHandler _errorHandler;
    private readonly Lazy<INavigationHelper> _navigationHelper;
    private readonly IIdDisplayHandler _idDisplayHandler;
    

    public PromptForBookingId(
        IBookingIdRenderer bookingIdRenderer,
        IErrorHandler errorHandler,
        Lazy<INavigationHelper> navigationHelper,
         IIdDisplayHandler idDisplayHandler

        )
    {
        _bookingIdRenderer = bookingIdRenderer;
        _errorHandler = errorHandler;
        _navigationHelper = navigationHelper;
        _idDisplayHandler = idDisplayHandler;
    }

    public int GetValidBookingId(string actionDescription, string whoCalling)
    {
        while (true)
        {
            Console.Clear();
            if (whoCalling == "date")
            {
            _bookingIdRenderer.DisplayBookingNumber(actionDescription);
            }else if(whoCalling == "GuestAssignmentHandler")
            {
                _idDisplayHandler.DisplayRightAligned(actionDescription);
            }

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

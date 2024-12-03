using Autofac.Features.AttributeFilters;
using HavenHotel.Guests;
using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Bookings.Services.Delete
{
    public class DeleteBooking : IDelete
    {
        private readonly IRepository<Booking> _bookingRepo;
        private readonly IDisplayRight _displayRight;
        private readonly IErrorHandler _errorHandler;
        private readonly IUserMessages _userMessages;
        private readonly INavigationHelper _navigationHelper;

        public DeleteBooking
            (
            IRepository<Booking> bookingRepo,
            [KeyFilter("DisplayBookingsIDRight")] IDisplayRight displayRight,
            IErrorHandler errorHandler,
            IUserMessages userMessages,
            INavigationHelper navigationHelper
            )
        {
            _bookingRepo = bookingRepo;
            _displayRight = displayRight;
            _errorHandler = errorHandler;
            _userMessages = userMessages;
            _navigationHelper = navigationHelper;
        }
        public void Delete()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    _displayRight.DisplayRightAligned("booking");
                    var bookingsLength = _bookingRepo.GetAllItems().ToList().Count + 1;


                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("===== DELETE A BOOKING =====");
                    _userMessages.ShowCancelMessage();
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write("Please enter the booking ID: ");
                    string idInput = Console.ReadLine();
                    _navigationHelper.ReturnToMenu(idInput);
                    if (int.TryParse(idInput, out int id) && id >= 0 && id < bookingsLength)
                    {

                        _bookingRepo.RemoveItemById(id);
                        _bookingRepo.SaveChanges();

                        Console.Write("Press any key to return to menu...");
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
}

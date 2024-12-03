using Autofac.Features.AttributeFilters;
using HavenHotel.Common;
using HavenHotel.Guests;
using HavenHotel.Interfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;

namespace HavenHotel.Bookings.Services.Display
{
    public class DisplayBookingDetails : IDisplay
    {
        private readonly IRepository<Booking> _bookingRepo;
        private readonly IDisplayRight _displayRight;
        private readonly IErrorHandler _errorHandler;
        private readonly IUserMessages _userMessages;
        private readonly INavigationHelper _navigationHelper;
        private readonly IRepository<Room> _roomsRepo;
        private readonly IRepository<Guest> _guestsRepo;

        public DisplayBookingDetails
            (
            IRepository<Booking> bookingRepo,
            [KeyFilter("DisplayGuestsRight")] IDisplayRight displayRight,
            IErrorHandler errorHandler,
            IUserMessages userMessages,
            INavigationHelper navigationHelper,
            IRepository<Room> roomsRepo,
            IRepository<Guest> guestRepo

            )
        {
            _bookingRepo = bookingRepo;
            _displayRight = displayRight;
            _errorHandler = errorHandler;
            _userMessages = userMessages;
            _navigationHelper = navigationHelper;
            _roomsRepo = roomsRepo;
            _guestsRepo = guestRepo;

        }
        public void DisplayById()
        {
            int invalidCounter = 0;
            while (true)
            {
                try
                {
                    Console.Clear();
                    _displayRight.DisplayRightAligned("booking");


                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("===== DISPLAY A BOOKING =====");
                    _userMessages.ShowCancelMessage();
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write("Please enter the booking ID: ");
                    string idInput = Console.ReadLine();
                    _navigationHelper.ReturnToMenu(idInput);
                    if (int.TryParse(idInput, out int id))
                    {
                        var booking = _bookingRepo.GetItemById(id);
                        var room = _roomsRepo.GetItemById(booking.Id);
                        var guest = _guestsRepo.GetItemById(booking.Id);
                        int totalDays = (booking.EndDate.ToDateTime(TimeOnly.MinValue) - booking.StartDate.ToDateTime(TimeOnly.MinValue)).Days;
                        var totalPrice = totalDays * room.Price;


                        Console.Clear();
                        Console.WriteLine("╔═════════════════╦═════════════╦════════════╦══════════════╦═══════════════╗");
                        Console.WriteLine("║ Customer Name   ║ Room Type   ║ Start Date ║  End Date    ║ Total price   ║");
                        Console.WriteLine("╠═════════════════╬═════════════╬════════════╬══════════════╬═══════════════╣");
                        Console.ForegroundColor = ConsoleColor.Gray;

                        Console.WriteLine($"║ {guest.Name,-15} ║  {room.RoomType,-10} ║ {booking.StartDate:yyyy-MM-dd} ║ {booking.EndDate:yyyy-MM-dd}   ║ {totalPrice,-13:C} ║");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("╚═════════════════╩═════════════╩════════════╩══════════════╩═══════════════╝");

                        Console.ResetColor();

                        Console.Write("Press any key to return to menu...");
                        Console.ReadKey();
                        return;

                    }
                    else if (invalidCounter >= 2)
                    {
                        _errorHandler.DisplayError("List's on the right ->. " +
                            "\nChoose an ID from the list please:)");
                        invalidCounter = 0;
                    }
                    else
                    {
                        invalidCounter++;
                        _errorHandler.DisplayError("Invalid id input, try again...");
                    }


                }
                catch (Exception)
                {
                    invalidCounter++;
                    _errorHandler.DisplayError("Invalid id number input, try again...");
                }
            }
        }
    }
}
using Autofac.Features.AttributeFilters;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Controllers.BookingController.Create;

public class CreateBooking : ICreate
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Room> _roomRepo;
    private readonly IRepository<Guest> _guestRepo;
    private readonly Lazy<INavigationHelper> _navigationHelper;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IDateValidator _dateValidator;
    private readonly IErrorHandler _errorHandler;
    private readonly IBookingSidebarDisplay _bookingSidebarDisplay;

    public CreateBooking(
        IRepository<Guest> guestRepo,
        IRepository<Room> roomRepo,
        IRepository<Booking> bookingRepo,
        Lazy<INavigationHelper> navigationHelper,
        [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
        IDateValidator dateValidator,
        IErrorHandler errorHandler,
        IBookingSidebarDisplay bookingSidebarDisplay
    )
    {
        _guestRepo = guestRepo;
        _roomRepo = roomRepo;
        _bookingRepo = bookingRepo;
        _navigationHelper = navigationHelper;
        _errorHandler = errorHandler;
        _bookingSidebarDisplay = bookingSidebarDisplay;
        _mainMenu = mainMenu;
        _dateValidator = dateValidator;
    }

    public void Create()
    {
        bool isDate = true;

        while (true)
        {
            Console.Clear();
            try
            {
                _bookingSidebarDisplay.DisplayRightAligned("CREATE NEW BOOKING");

                Console.Write("Enter Guest ID: ");
                string idInput = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(idInput);

                if (!int.TryParse(idInput, out int id) || !IsActiveGuest(id))
                {
                    _errorHandler.DisplayError("Invalid Guest ID. Please try again.");
                    continue;
                }

                Console.Write("Room for how many guests: ");
                string guestsNum = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(guestsNum);

                if (!int.TryParse(guestsNum, out int totalGuests) || !IsTotalGuests(totalGuests) || totalGuests <= 0)
                {
                    _errorHandler.DisplayError("Invalid number of guests. Please try again.");
                    continue;
                }

                Console.Write("Enter room number: ");
                string roomNum = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(roomNum);

                if (!int.TryParse(roomNum, out int roomNumber) || !IsAppropriateRoom(roomNumber, totalGuests))
                {
                    _errorHandler.DisplayError("Invalid room number. Please try again.");
                    continue;
                }

                var room = _roomRepo.GetAllItems().FirstOrDefault(r => r.RoomNumber == roomNumber);
                if (room == null)
                {
                    _errorHandler.DisplayError("Room not found. Please try again.");
                    continue;
                }

                if (!isRoomAvailable(room.Id))
                {
                    var nextAvailableDate = GetNextAvailableDate(room.Id);
                    _errorHandler.DisplayError($"Room is not available. It will be available again after {nextAvailableDate:yyyy-MM-dd}.");
                    continue;
                }

                while (isDate)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Enter check-in date (yyyy-MM-dd): ");
                    string atDate = Console.ReadLine();
                    _navigationHelper.Value.ReturnToMenu(atDate);

                    if (!DateOnly.TryParse(atDate, out DateOnly checkInDate) || !_dateValidator.IsCorrectStartDate(checkInDate))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid check-in date. Please try again.");
                        Console.ResetColor();
                        continue;
                    }

                    Console.Write("Enter number of nights: ");
                    string nightsInput = Console.ReadLine();
                    _navigationHelper.Value.ReturnToMenu(nightsInput);

                    if (!int.TryParse(nightsInput, out int nights) || nights <= 0)
                    {
                        Console.WriteLine("Invalid number of nights. Please enter a positive number.");
                        return;
                    }

                    DateOnly checkOutDate = checkInDate.AddDays(nights);
                    decimal roomPrice = room.Price;
                    decimal totalPrice = nights * roomPrice;

                    var booking = new Booking
                    {
                        StartDate = checkInDate,
                        EndDate = checkOutDate,
                        TotalPrice = totalPrice,
                        GuestId = id,
                        RoomId = room.Id
                    };

                    room.IsActive = false;
                    _roomRepo.Update(room);

                    _bookingRepo.Add(booking);
                    _bookingRepo.SaveChanges();

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Room number {roomNumber} has been successfully booked.");
                    Console.WriteLine($"Total price: {totalPrice:C}");
                    Console.Write("Press any key to return to the menu...");
                    Console.ReadKey();
                    _mainMenu.Value.DisplayMenu();
                    Console.ResetColor();
                    isDate = false;
                }
            }
            catch (Exception ex)
            {
                _errorHandler.DisplayError($"Something went wrong: {ex.Message}");
            }
        }
    }

    private bool IsActiveGuest(int id)
    {
        return _guestRepo.GetAllItems().Any(g => g.IsActive && g.Id == id);
    }

    private bool IsTotalGuests(int totalGuests)
    {
        int maxGuests = _roomRepo.GetAllItems().Max(r => r.TotalGuests);
        return totalGuests <= maxGuests;
    }

    private bool IsAppropriateRoom(int roomNum, int totalGuest)
    {
        return _roomRepo.GetAllItems().Any(r => r.RoomNumber == roomNum && r.TotalGuests >= totalGuest);
    }

    private bool isRoomAvailable(int roomId)
    {
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

        var booking = _bookingRepo.GetAllItems().FirstOrDefault(b => b.RoomId == roomId);
        if (booking == null)
        {
            return true; 
        }

        return currentDate > booking.EndDate;
    }

    private DateOnly GetNextAvailableDate(int roomId)
    {
        var booking = _bookingRepo.GetAllItems().FirstOrDefault(b => b.RoomId == roomId);
        return booking?.EndDate ?? DateOnly.MinValue;
    }
}

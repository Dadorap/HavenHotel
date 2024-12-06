using Autofac.Features.AttributeFilters;
using HavenHotel.Guests;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Interfaces.RoomsInterfaces;
using HavenHotel.Repositories;
using HavenHotel.Rooms;

namespace HavenHotel.Bookings.Services.Create;

public class CreateBooking : ICreate
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Room> _roomRepo;
    private readonly IRepository<Guest> _guestRepo;
    private readonly Lazy<INavigationHelper> _navigationHelper;
    private readonly Lazy<IMenu> _mainMenu;
    private readonly IErrorHandler _errorHandler;
    private readonly IUserMessages _userMessages;
    private readonly IBookingSidebarDisplay _bookingSidebarDisplay;


    public CreateBooking(
        IRepository<Guest> guestRepo,
        IRepository<Room> roomRepo,
        IRepository<Booking> bookingRepo,
        Lazy<INavigationHelper> navigationHelper,
        [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
        IErrorHandler errorHandler,
        IUserMessages userMessages,
        IBookingSidebarDisplay bookingSidebarDisplay

        )
    {
        _guestRepo = guestRepo;
        _roomRepo = roomRepo;
        _bookingRepo = bookingRepo;
        _navigationHelper = navigationHelper;
        _errorHandler = errorHandler;
        _userMessages = userMessages;
        _bookingSidebarDisplay = bookingSidebarDisplay;
        _mainMenu = mainMenu;
    }

    public void Create()
    {
        bool isDate = true;
        while (true)
        {
            Console.Clear();
            try
            {
                _bookingSidebarDisplay.DisplayRightAligned();
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("CREATE NEW BOOKING");
                Console.ResetColor();
                _userMessages.ShowCancelMessage();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Enter Guests ID: ");
                string idInput = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(idInput);

                if (!int.TryParse(idInput, out int id) || !IsActiveGuest(id))
                {
                    _errorHandler.DisplayError("Invalid ID input try again...");
                    continue;
                }

                Console.Write("Room for how many guests: ");
                string guestsNum = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(guestsNum);

                if (!int.TryParse(guestsNum, out int totalGuests) || !IsTotalGuests(totalGuests))
                {
                    _errorHandler.DisplayError("Invalid total guest input try again...");
                    continue;
                }
                Console.Write("Enter room number: ");
                string roomNum = Console.ReadLine();
                _navigationHelper.Value.ReturnToMenu(roomNum);
                if (!int.TryParse(roomNum, out int roomNumber) || !IsAppropriateRoom(roomNumber, totalGuests))
                {
                    _errorHandler.DisplayError("Invalid room number try again...");
                    continue;
                }
                while (isDate)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Enter checkin date (yyyy-MM-dd)");
                    string atDate = Console.ReadLine();
                    _navigationHelper.Value.ReturnToMenu(atDate);
                    if (!DateOnly.TryParse(atDate, out DateOnly startDate) || !IsCorrectStartDate(startDate))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid date input try again...");
                        Console.ResetColor();
                        continue;
                    }

                    Console.WriteLine("Enter checkout date (yyyy-MM-dd)");
                    string lastDate = Console.ReadLine();
                    _navigationHelper.Value.ReturnToMenu(lastDate);
                    if (!DateOnly.TryParse(lastDate, out DateOnly endDate) || !IsCorrectEndDate(startDate, endDate))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid date input try again...");
                        Console.ResetColor();
                        continue;
                    }
                    var roomID = _roomRepo.GetAllItems().Where(r => r.RoomNumber == roomNumber).Select(r => r.Id).Single();
                    var rooms = _roomRepo.GetAllItems().Where(r => r.Id == roomID).ToList();
                    var roomPrice = _roomRepo.GetItemById(roomID).Price;
                    int totalDays = (endDate.ToDateTime(TimeOnly.MinValue) -
                                    startDate.ToDateTime(TimeOnly.MinValue)).Days;
                    var daysTotal = totalDays == 0 ? 1 : totalDays;
                    var totalPrice = daysTotal * roomPrice;

                    var booking = new Booking
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        TotalPrice = totalPrice,
                        GuestId = id,
                        RoomId = roomID
                    };
                    rooms.ForEach(r =>
                    {
                        r.IsActive = false;
                        _roomRepo.Update(r);
                    });
                    _bookingRepo.Add(booking);                    
                    _bookingRepo.SaveChanges();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Room number {roomNumber} has been successfully booked.\nTotal price: {totalPrice:C}");
                    Console.Write("Press any key to return to menu...");
                    Console.ReadKey();
                    _mainMenu.Value.DisplayMenu();
                    Console.ResetColor();
                    isDate = false;  
                }
            }
            catch (Exception ex)
            {
                _errorHandler.DisplayError($"Somehting went wrong. {ex.Message}");
                continue;
            }
        }
    }
    private bool IsActiveGuest(int id)
    {
        var guest = _guestRepo.GetAllItems().Any(g => g.IsActive && g.Id == id);
        return guest;
    }
    private bool IsTotalGuests(int toatalGuests)
    {
        var maxG = _roomRepo.GetAllItems().Max(g => g.TotalGuests);
        bool isTotalGuest = toatalGuests <= maxG;
        return isTotalGuest;
    }
    private bool IsAppropriateRoom(int roomNum, int totalGuest)
    {
        var appropriateRoom = _roomRepo.GetAllItems().Any(r => r.RoomNumber == roomNum && r.TotalGuests >= totalGuest);
        return appropriateRoom;
    }
    private bool IsCorrectStartDate(DateOnly dateOnly)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        bool isCorrectDate = dateOnly >= today;

        return isCorrectDate;
    }
    private bool IsCorrectEndDate(DateOnly startDate, DateOnly endDate)
    {
        bool isCorrectDate = endDate >= startDate;

        return isCorrectDate;
    }
}

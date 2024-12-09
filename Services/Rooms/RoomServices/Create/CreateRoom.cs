using Autofac.Features.AttributeFilters;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Services.RoomServices.Services.Create
{
    public class CreateRoom : ICreate
    {
        private readonly IRepository<Room> _roomRepo;
        private readonly Lazy<INavigationHelper> _navigationHelper;
        private readonly Lazy<IMenu> _mainMenu;
        private readonly IErrorHandler _errorHandler;
        private readonly IUserMessages _userMessages;
        private readonly IDisplayRoomNumRight _displayRoomNumRight;


        public CreateRoom(
            IRepository<Room> roomRepo,
            Lazy<INavigationHelper> navigationHelper,
            [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
            IErrorHandler errorHandler,
            IUserMessages userMessages,
            IDisplayRoomNumRight displayRoomNumRight
            )
        {
            _roomRepo = roomRepo;
            _navigationHelper = navigationHelper;
            _errorHandler = errorHandler;
            _userMessages = userMessages;
            _displayRoomNumRight = displayRoomNumRight;
            _mainMenu = mainMenu;
        }

        public void Create()
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    _displayRoomNumRight.DisplayRightAligned();
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("CREATE NEW ROOM");
                    Console.ResetColor();
                    _userMessages.ShowCancelMessage();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;

                    Console.Write("Enter room type " +
                        "\n(Single, Double, Suite, Family): ");
                    string roomTypeInput = Console.ReadLine();
                    _navigationHelper.Value.ReturnToMenu(roomTypeInput);
                    if (!Enum.TryParse(roomTypeInput, true, out RoomType roomType) || !Enum.IsDefined(typeof(RoomType), roomType))
                    {
                        _errorHandler.DisplayError("Invalid input. Please enter a valid room type.");
                        continue;
                    }
                    (int minSize, int maxSize) = roomType switch
                    {
                        RoomType.SINGLE => (12, 15),
                        RoomType.DOUBLE => (15, 25),
                        RoomType.SUITE => (25, 30),
                        RoomType.FAMILY => (30, 50),
                        _ => throw new ArgumentException("Invalid room type")
                    };
                    Console.Write("Enter a room number (100 - 500): ");
                    string roomNumber = Console.ReadLine();
                    _navigationHelper.Value.ReturnToMenu(roomNumber);

                    if (!int.TryParse(roomNumber, out int roomNum))
                    {
                        _errorHandler.DisplayError("Invalid input. Please enter a valid number.");
                        continue;
                    }

                    if (roomNum < 100 || roomNum > 500)
                    {
                        _errorHandler.DisplayError("Room number must be between 100 and 500. Try again...");
                        continue;
                    }

                    var IsRoomNum = _roomRepo.GetAllItems()?.Any(r => r.RoomNumber == roomNum) ?? false;

                    if (IsRoomNum)
                    {
                        _errorHandler.DisplayError("Room number already exists. Try again...");
                        continue;
                    }


                    Console.WriteLine($"Enter room size {minSize}-{maxSize}:");
                    string roomSize = Console.ReadLine();
                    _navigationHelper.Value.ReturnToMenu(roomSize);
                    if (!int.TryParse(roomSize, out int size) || size < 12 || size > 50)
                    {
                        _errorHandler.DisplayError("Invalid room size. Please enter a value between 12 and 50.");
                        continue;
                    }


                    if (size < minSize || size > maxSize)
                    {
                        _errorHandler.DisplayError($"Invalid room size. A {roomType} must be between {minSize} and {maxSize} m².");
                        continue;
                    }

                    int maxGuests = roomType switch
                    {
                        RoomType.SINGLE => 1,
                        RoomType.DOUBLE => 2,
                        RoomType.SUITE => 4,
                        RoomType.FAMILY => 6,
                        _ => throw new ArgumentException("Invalid room type")
                    };

                    bool allowExtraBeds = roomType != RoomType.SINGLE;

                    Console.Write("Enter price per night (1000 - 5000): ");
                    string roomPrice = Console.ReadLine();
                    _navigationHelper.Value.ReturnToMenu(roomPrice);
                    if (!decimal.TryParse(roomPrice, out decimal price) || price < 1000 || price > 5000)
                    {
                        _errorHandler.DisplayError("Invalid price. Please enter a value between 100 and 999.");
                        continue;
                    }

                    int maxExtraBeds = size switch
                    {
                        <= 15 => 1,
                        <= 25 => 2,
                        _ => 3
                    };

                    int extraBeds = 0;
                    if (allowExtraBeds)
                    {
                        Console.WriteLine($"Enter the number of extra beds (0-{maxExtraBeds}):");
                        string extraBed = Console.ReadLine();
                        _navigationHelper.Value.ReturnToMenu(extraBed);
                        if (!int.TryParse(extraBed, out extraBeds) || extraBeds < 0 || extraBeds > maxExtraBeds)
                        {
                            _errorHandler.DisplayError($"Invalid input for extra beds. Please enter a value between 0 and {maxExtraBeds}.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Extra beds are not allowed for Single rooms. " +
                            "\nSkipping this step.");
                    }

                    int maxGuestsBySize = size switch
                    {
                        <= 12 => 2,
                        <= 20 => 3,
                        <= 30 => 4,
                        _ => 6
                    };

                    int totalCapacity = Math.Min(maxGuests + extraBeds, maxGuestsBySize);
                    Console.WriteLine($"How many guests are allowed in the room (1-{totalCapacity}):");
                    string guestTotal = Console.ReadLine();
                    _navigationHelper.Value.ReturnToMenu(guestTotal);
                    if (!int.TryParse(guestTotal, out int totalGuests) || totalGuests < 1 || totalGuests > totalCapacity)
                    {
                        _errorHandler.DisplayError($"Invalid input for total guests. Please enter a value between 1 and {totalCapacity}.");
                        continue;
                    }

                    if (roomType == RoomType.SUITE && totalGuests < 2)
                    {
                        _errorHandler.DisplayError("A Suite must have at least 2 guests.");
                        continue;
                    }

                    var room = new Room
                    {
                        RoomType = roomType,
                        RoomNumber = roomNum,
                        Price = price,
                        Size = size,
                        ExtraBed = extraBeds,
                        TotalGuests = totalGuests,
                        IsActive = true
                    };

                    _roomRepo.Add(room);
                    _roomRepo.SaveChanges();

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Room successfully created!");
                    Console.WriteLine($"Type: {roomType}, Price: {price}, Size: {size} m², " +
                        $"\nExtra Beds: {extraBeds}, Guests: {totalGuests}");
                    Console.ResetColor();

                    Console.Write("Press any key to return to the menu...");
                    Console.ReadKey();
                    _mainMenu.Value.DisplayMenu();
                    break;
                }
                catch (Exception ex)
                {
                    _errorHandler.DisplayError($"An error occurred: {ex.Message}");
                }
            }
        }

    }
}

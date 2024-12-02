﻿using HavenHotel.Interfaces;
using System;
using System.Linq;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using System.Threading.Channels;

namespace HavenHotel.Rooms.RoomServices
{
    public class CreateRoom : ICreate
    {
        private readonly IRepository<Room> _repository;
        private readonly IErrorHandler _errorHandler;
        private readonly IMainMenu _mainMenu;

        public CreateRoom(IRepository<Room> repository, IErrorHandler errorHandler, IMainMenu mainMenu)
        {
            _repository = repository;
            _errorHandler = errorHandler;
            _mainMenu = mainMenu;
        }

        public void Create()
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("CREATE NEW ROOM");
                    Console.ResetColor();
                    Console.WriteLine("Enter 'cancel' at any time to return to the main menu.");


                    Console.WriteLine("Enter room type (Single, Double, Suite, Family):");
                    string roomTypeInput = Console.ReadLine();
                    ReturnToMenu(roomTypeInput);
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

                    Console.WriteLine($"Enter room size {minSize}-{maxSize}:");
                    string roomSize = Console.ReadLine(); ;
                    ReturnToMenu(roomSize);
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

                    Console.WriteLine("Enter price per day (100 - 999):");
                    string roomPrice = Console.ReadLine();
                    ReturnToMenu(roomPrice);
                    if (!decimal.TryParse(roomPrice, out decimal price) || price < 100 || price > 999)
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
                        Console.WriteLine($"How many extra beds are allowed in the room (0-{maxExtraBeds}):");
                        string extraBed = Console.ReadLine();
                        ReturnToMenu(extraBed);
                        if (!int.TryParse(extraBed, out extraBeds) || extraBeds < 0 || extraBeds > maxExtraBeds)
                        {
                            _errorHandler.DisplayError($"Invalid input for extra beds. Please enter a value between 0 and {maxExtraBeds}.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Extra beds are not allowed for Single rooms. Skipping this step.");
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
                    ReturnToMenu(guestTotal);
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
                        Price = price,
                        Size = size,
                        ExtraBed = extraBeds,
                        TotalGuests = totalGuests,
                        IsAvailable = true
                    };

                    _repository.Add(room);
                    _repository.SaveChanges();

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Room successfully created!");
                    Console.WriteLine($"Type: {roomType}, Price: {price}, Size: {size} m², " +
                        $"\nExtra Beds: {extraBeds}, Guests: {totalGuests}");
                    Console.ResetColor();

                    Console.Write("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;
                }
                catch (Exception ex)
                {
                    _errorHandler.DisplayError($"An error occurred: {ex.Message}");
                }
            }
        }
        private void ReturnToMenu(string input)
        {
            if (input.ToLower() == "cancel")
            {
                _mainMenu.DisplayMenu();
                return;
            }
        }



    }
}

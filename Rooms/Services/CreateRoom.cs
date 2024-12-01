using HavenHotel.Interfaces;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using HavenHotel.Repositories;
using HavenHotel.Rooms;
using System.Threading.Channels;

namespace HavenHotel.Rooms.RoomServices
{
    public class CreateRoom : ICreate
    {
        private readonly IRepository<Room> _repository;

        public CreateRoom(IRepository<Room> repository)
        {
            _repository = repository;
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

                    Console.WriteLine("Enter room type (Single, Double, Suite, Family):");
                    string roomTypeInput = Console.ReadLine()?.Trim();

                    if (!Enum.TryParse(roomTypeInput, true, out RoomType roomType) || !Enum.IsDefined(typeof(RoomType), roomType))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a valid room type.");
                        Console.ResetColor();
                        Console.Write("Press any key to return...");
                        Console.ReadKey();
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
                    if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 100 || price > 999)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid price. Please enter a value between 100 and 999.");
                        Console.ResetColor();
                        Console.Write("Press any key to return...");
                        Console.ReadKey();
                        continue;
                    }

                    Console.WriteLine("Enter room size (12-50 m²):");
                    if (!int.TryParse(Console.ReadLine(), out int size) || size < 12 || size > 50)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid room size. " +
                            "\nPlease enter a value between 12 and 50.");
                        Console.ResetColor();
                        Console.Write("Press any key to return...");
                        Console.ReadKey();
                        continue;
                    }

                    int extraBeds = 0;
                    if (allowExtraBeds)
                    {
                        Console.WriteLine($"How many extra beds are allowed in the room (0-{maxGuests}):");
                        if (!int.TryParse(Console.ReadLine(), out extraBeds) 
                            || extraBeds < 0 
                            || extraBeds > maxGuests)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Invalid input for extra beds. " +
                                $"\nPlease enter a value between 0 and {maxGuests}.");
                            Console.ResetColor();
                            Console.Write("Press any key to return...");
                            Console.ReadKey();
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Extra beds are not allowed for Single rooms.");
                    }

                    Console.WriteLine($"How many guests are allowed in the room (1-{maxGuests + extraBeds}):");
                    if (!int.TryParse(Console.ReadLine(), out int totalGuests) 
                        || totalGuests < 1 
                        || totalGuests > (maxGuests + extraBeds))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Invalid input for total guests. " +
                            $"\nPlease enter a value between 1 and {maxGuests}.");
                        Console.ResetColor();
                        Console.Write("Press any key to return...");
                        Console.ReadKey();
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
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.ResetColor();
                    Console.Write("Press any key to return...");
                    Console.ReadKey();
                }
            }
        }


    }
}
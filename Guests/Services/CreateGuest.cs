using HavenHotel.Interfaces;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using HavenHotel.Repositories;
using HavenHotel.Common;

namespace HavenHotel.Guests.GuestServices
{
    public class CreateGuest : ICreate
    {
        private readonly IRepository<Guest> _repository;
        private readonly INavigationHelper _navigationHelper;


        public CreateGuest(IRepository<Guest> repository, INavigationHelper navigationHelper)
        {
            _repository = repository;
            _navigationHelper = navigationHelper;
           
        }

        public void Create()
        {

            while (true)
            {
                Console.Clear();
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("CREATE NEW GUEST");
                    Console.ResetColor();
                    Console.Write("Enter ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("'cancel' ");
                    Console.ResetColor();
                    Console.WriteLine("at any time to return to the main menu.");
                    Console.ForegroundColor = ConsoleColor.Blue;

                    Console.WriteLine("Enter guest's name:");
                    var guestName = Console.ReadLine()?.Trim();
                    _navigationHelper.ReturnToMenu(guestName);
                    if (string.IsNullOrWhiteSpace(guestName))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid name. Please enter a valid name.");
                        Console.ResetColor();
                        Console.Write("Press any key to return...");
                        Console.ReadKey();
                        continue;
                    }

                    Console.WriteLine("Enter phone number:");
                    string phoneNumber = Console.ReadLine();
                    _navigationHelper.ReturnToMenu(phoneNumber);
                    if (string.IsNullOrWhiteSpace(phoneNumber)
                        || phoneNumber.Length < 3
                        || phoneNumber.Length > 15
                        || !phoneNumber.All(char.IsDigit))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid phone number. " +
                            "\nPlease enter a number between 3 and 15 digits.");
                        Console.ResetColor();
                        Console.Write("Press any key to return...");
                        Console.ReadKey();
                        continue;
                    }

                    Console.WriteLine("Enter email address:");
                    var email = Console.ReadLine()?.Trim();
                    _navigationHelper.ReturnToMenu(email);

                    if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid email address. " +
                            "\nPlease enter a valid email.");
                        Console.ResetColor();
                        Console.Write("Press any key to return...");
                        Console.ReadKey();
                        continue;
                    }

                    var guest = new Guest
                    {
                        Name = guestName,
                        PhoneNumber = phoneNumber,
                        Email = email
                    };

                    _repository.Add(guest);
                    _repository.SaveChanges();
                    Console.Clear();
                    Console.WriteLine("\nGuest successfully created:");
                    Console.WriteLine($"Name: {guest.Name}");
                    Console.WriteLine($"Phone: {guest.PhoneNumber}");
                    Console.WriteLine($"Email: {guest.Email}");

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
        private bool IsValidEmail(string email)
        {
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }

    }
}

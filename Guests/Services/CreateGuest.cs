using HavenHotel.Interfaces;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using HavenHotel.Repositories;
using HavenHotel.Common;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Rooms;

namespace HavenHotel.Guests.GuestServices
{
    public class CreateGuest : ICreate
    {
        private readonly IRepository<Guest> _guestRepo;
        private readonly Lazy<INavigationHelper> _navigationHelper;
        private readonly IErrorHandler _errorHandler;
        private readonly IUserMessages _userMessages;


        public CreateGuest(
            IRepository<Guest> guestRepo,
            Lazy<INavigationHelper> navigationHelper,
            IErrorHandler errorHandler,
            IUserMessages userMessages
            )
        {
            _guestRepo = guestRepo;
            _navigationHelper = navigationHelper;
            _errorHandler = errorHandler;
            _userMessages = userMessages;
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
                    _userMessages.ShowCancelMessage();
                    Console.ForegroundColor = ConsoleColor.Blue;

                    Console.WriteLine("Enter guest's name:");
                    var guestName = Console.ReadLine()?.Trim();
                    _navigationHelper.Value.ReturnToMenu(guestName);
                    if (string.IsNullOrWhiteSpace(guestName))
                    {
                        _errorHandler.DisplayError("Invalid name. Please enter a valid name.");
                        continue;
                    }

                    Console.WriteLine("Enter phone number:");
                    string phoneNumber = Console.ReadLine();
                    _navigationHelper.Value.ReturnToMenu(phoneNumber);
                    if (string.IsNullOrWhiteSpace(phoneNumber)
                        || phoneNumber.Length < 3
                        || phoneNumber.Length > 15
                        || !phoneNumber.All(char.IsDigit))
                    {
                        _errorHandler.DisplayError("Invalid phone number. " +
                            "\nPlease enter a number between 3 and 15 digits.");
                        continue;
                    }

                    Console.WriteLine("Enter email address:");
                    var email = Console.ReadLine()?.Trim();
                    _navigationHelper.Value.ReturnToMenu(email);

                    if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
                    {
                        _errorHandler.DisplayError("Invalid email address. " +
                            "\nPlease enter a valid email.");
                        continue;
                    }

                    var guest = new Guest
                    {
                        Name = guestName,
                        PhoneNumber = phoneNumber,
                        Email = email
                    };

                    _guestRepo.Add(guest);
                    _guestRepo.SaveChanges();
                    Console.Clear();
                    Console.WriteLine("\nGuest successfully created:");
                    Console.WriteLine($"Name: {guest.Name}");
                    Console.WriteLine($"Phone: {guest.PhoneNumber}");
                    Console.WriteLine($"Email: {guest.Email}");

                    break;
                }
                catch (Exception ex)
                {
                    _errorHandler.DisplayError($"An error occurred: {ex.Message}");
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

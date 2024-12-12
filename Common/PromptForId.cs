using Autofac.Features.AttributeFilters;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Common
{
    public class PromptForId : IPromptForId
    {
        private readonly IDisplayRight _displayRight;
        private readonly IUserMessages _userMessages;
        private readonly IErrorHandler _errorHandler;
        private readonly Lazy<INavigationHelper> _navigationHelper;
        private readonly IRepository<Guest> _guestRepo;
        private readonly IRepository<Room> _roomRepo;
        private readonly IRepository<Booking> _bookingRepo;




        public PromptForId
            (
            IDisplayRight displayRight,
            IUserMessages userMessages,
            Lazy<INavigationHelper> navigationHelper,
            IErrorHandler errorHandler,
            IRepository<Guest> guestRepo,
            IRepository<Room> roomRepo,
            IRepository<Booking> bookingRepo



            )
        {
            _displayRight = displayRight;
            _userMessages = userMessages;
            _errorHandler = errorHandler;
            _navigationHelper = navigationHelper;
            _guestRepo = guestRepo;
            _roomRepo = roomRepo;
            _bookingRepo = bookingRepo;

        }

        public int GetValidId(string headerText, string identifier)
        {
            while (true)
            {

                try
                {
                    Console.Clear();
                    var headerT = $"===== {headerText} Update Handler =====".ToUpper();
                    _displayRight.DisplayRightAligned(identifier, "all");
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(headerT);
                    Console.ResetColor();
                    _userMessages.ShowCancelMessage();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write($"Enter {identifier} ID: ");
                    string inputId = Console.ReadLine();
                    _navigationHelper.Value.ReturnToMenu(inputId);
                    if (!int.TryParse(inputId, out int id) || !IsIdFound(id, identifier))
                    {
                        _errorHandler.DisplayError("Invalid ID input. Try again...");
                        continue;
                    }

                    return id;

                }
                catch (Exception ex)
                {
                    _errorHandler.DisplayError(ex.Message);
                }
            }
        }
        private bool IsIdFound(int id, string identifier)
        {
            if (identifier == "guest")
            {
                var isGuestFound = _guestRepo.GetAllItems().Any(i => i.Id == id);
                return isGuestFound;
            }
            else if(identifier == "room")
            {
                var isFound = _roomRepo.GetAllItems().Any(i => i.Id == id);
                return isFound;
            }
            else
            {
                var isFound = _bookingRepo.GetAllItems().Any(i => i.Id == id);
                return isFound;
            }
        }
    }
}

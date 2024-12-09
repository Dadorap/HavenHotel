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

namespace HavenHotel.Services.Guests
{
    public class PromptForGuestId : IPromptForGuestId
    {
        private readonly IDisplayRight _displayRight;
        private readonly IUserMessages _userMessages;
        private readonly IErrorHandler _errorHandler;
        private readonly Lazy<INavigationHelper> _navigationHelper;
        private readonly IRepository<Guest> _guestRepo;




        public PromptForGuestId
            (
            [KeyFilter("DisplayGuestsIDRight")] IDisplayRight displayRight,
            IUserMessages userMessages,
            Lazy<INavigationHelper> navigationHelper,
            IErrorHandler errorHandler,
            IRepository<Guest> guestRepo

            )
        {
            _displayRight = displayRight;
            _userMessages = userMessages;
            _errorHandler = errorHandler;
            _navigationHelper = navigationHelper;
            _guestRepo = guestRepo;

        }

        public int GetValidGuestId(string headerText)
        {
            while (true)
            {

                try
                {
                    Console.Clear();
                    var headerT = $"===== {headerText} Update Handler =====".ToUpper();
                    _displayRight.DisplayRightAligned("guest", "all");
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(headerT);
                    Console.ResetColor();
                    _userMessages.ShowCancelMessage();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("Enter guest ID: ");
                    string inputId = Console.ReadLine();
                    _navigationHelper.Value.ReturnToMenu(inputId);
                    if (!int.TryParse(inputId, out int id) || !IsIdFound(id))
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
        private bool IsIdFound(int id)
        {
            var isFound = _guestRepo.GetAllItems().Any(i => i.Id == id);
            return isFound;
        }
    }
}

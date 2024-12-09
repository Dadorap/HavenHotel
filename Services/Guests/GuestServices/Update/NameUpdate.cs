using Autofac.Features.AttributeFilters;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Services.Guests.GuestServices.Update
{
    public class NameUpdate : INameUpdate
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IPromptForGuestId _promptForGuestId;
        private readonly IRepository<Guest> _guestRepo;
        private readonly Lazy<IMenu> _mainMenu;

        public NameUpdate
            (
            IErrorHandler errorHandler,
            IPromptForGuestId promptForGuestId,
            IRepository<Guest> guestRepo,
            [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu
            )
        {
            _errorHandler = errorHandler;
            _promptForGuestId = promptForGuestId;
            _guestRepo = guestRepo;
            _mainMenu = mainMenu;
        }

        public void NameUpdater()
        {
            while (true)
            {
                try
                {
                    var id = _promptForGuestId.GetValidGuestId("name");
                    var currentGuest = _guestRepo.GetItemById(id);
                    if (currentGuest == null)
                    {
                        _errorHandler.DisplayError("Guest not found. Try again...");
                        continue;
                    }
                    Console.WriteLine($"Guest's full name: {currentGuest.Name}");
                    Console.Write("Enter new full name: ");
                    string nameInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(nameInput.Trim()))
                    {
                        _errorHandler.DisplayError("Invalid name input. Try again...");
                        continue;
                    }
                    
                    currentGuest.Name = nameInput;
                    _guestRepo.SaveChanges();

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Guest's name has been updated to: {currentGuest.Name}");
                    Console.ResetColor();
                    Console.Write("Press any key to return to menu...");
                    Console.ReadKey();
                    _mainMenu.Value.DisplayMenu();
                    break;
                }
                catch (Exception ex)
                {
                    _errorHandler.DisplayError(ex.Message);
                }
            }
        }
    }
}

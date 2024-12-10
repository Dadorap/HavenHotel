using Autofac.Features.AttributeFilters;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.GuestInterfaces;
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
        private readonly IPromptForId _promptForGuestId;
        private readonly IRepository<Guest> _guestRepo;
        private readonly IUpdateConfirmation _updateConfirmation;
        private readonly Lazy<INavigationHelper> _navigationHelper;


        public NameUpdate
            (
            IErrorHandler errorHandler,
            IPromptForId promptForGuestId,
            IRepository<Guest> guestRepo,
            IUpdateConfirmation updateConfirmation,
            Lazy<INavigationHelper> navigationHelper)
        {
            _errorHandler = errorHandler;
            _promptForGuestId = promptForGuestId;
            _guestRepo = guestRepo;
            _updateConfirmation = updateConfirmation;
            _navigationHelper = navigationHelper;
        }

        public void NameUpdater()
        {
            while (true)
            {
                try
                {
                    var id = _promptForGuestId.GetValidId("name", "guest");
                    var currentGuest = _guestRepo.GetItemById(id);
                    if (currentGuest == null)
                    {
                        _errorHandler.DisplayError("Guest not found. Try again...");
                        continue;
                    }
                    Console.WriteLine($"Guest's full name: {currentGuest.Name}");
                    Console.Write("Enter new full name: ");
                    string nameInput = Console.ReadLine().Trim();
                    _navigationHelper.Value.ReturnToMenu(nameInput);
                    if (string.IsNullOrEmpty(nameInput))
                    {
                        _errorHandler.DisplayError("Invalid name input. Try again...");
                        continue;
                    }
                    
                    currentGuest.Name = nameInput;
                    _guestRepo.SaveChanges();
                    _updateConfirmation.Confirmation($"Guest's name has been updated to: {currentGuest.Name}");
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

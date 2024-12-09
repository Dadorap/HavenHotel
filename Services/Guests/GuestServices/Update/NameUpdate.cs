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
        private readonly IUpdateConfirmation _updateConfirmation;

        public NameUpdate
            (
            IErrorHandler errorHandler,
            IPromptForGuestId promptForGuestId,
            IRepository<Guest> guestRepo,
            IUpdateConfirmation updateConfirmation
            )
        {
            _errorHandler = errorHandler;
            _promptForGuestId = promptForGuestId;
            _guestRepo = guestRepo;
            _updateConfirmation = updateConfirmation;
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

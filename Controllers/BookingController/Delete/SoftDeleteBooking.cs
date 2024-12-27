using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Interfaces.DeleteInterfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Controllers.BookingController.Delete;

public class SoftDeleteBooking : ISoftDelete
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Room> _roomRepo;
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptForId;
    private readonly IUpdateConfirmation _updateConfirmation;


    public SoftDeleteBooking
        (
        IRepository<Booking> repository,
        IErrorHandler errorHandler,
        IPromptForId promptForId,
        IUpdateConfirmation updateConfirmation
,
        IRepository<Room> roomRepo)
    {
        _bookingRepo = repository;
        _errorHandler = errorHandler;
        _promptForId = promptForId;
        _updateConfirmation = updateConfirmation;
        _roomRepo = roomRepo;
    }
    public void SoftDelete()
    {
        while (true)
        {
            try
            {
                var id = _promptForId.GetValidId("soft delete", "booking", "true");
                var currentBooking = _bookingRepo.GetItemById(id);
                if (currentBooking == null) continue;

                var booking = _bookingRepo.GetAllItems().FirstOrDefault(b => b.Id == id);
                if (currentBooking != null && !currentBooking.IsPaid)
                {
                    _errorHandler.DisplayError("Invoice not paid." +
                        "\nThe invoice must be paid to proceed with this action.");
                    continue;
                }
                
                var room = _roomRepo.GetItemById(currentBooking.RoomId);

                if (currentBooking.IsActive)
                {
                    room.IsActive = true;
                    _roomRepo.Update(room);
                    currentBooking.IsActive = false;
                    _bookingRepo.SaveChanges();
                    _updateConfirmation.Confirmation($"Booking with ID: {currentBooking.Id}. " +
                        $"\nHas been soft-deleted successfully.");
                    break;
                }
                else
                {
                    _errorHandler.DisplayError("ID already soft-deleted. " +
                   "\nPlease try again.");
                    continue;
                }


            }
            catch (Exception)
            {
                _errorHandler.DisplayError("Invalid input. Try again.");
            }
        }


    }
}

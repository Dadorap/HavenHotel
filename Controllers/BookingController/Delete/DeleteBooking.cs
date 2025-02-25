﻿using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Interfaces.DeleteInterfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Controllers.BookingController.Delete;

public class DeleteBooking : IDelete
{
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Room> _roomRepo;
    private readonly IErrorHandler _errorHandler;
    private readonly IPromptForId _promptForId;
    private readonly IUpdateConfirmation _updateConfirmation;


    public DeleteBooking
        (
        IRepository<Booking> repository,
        IErrorHandler errorHandler,
        IPromptForId promptForId,
        IUpdateConfirmation updateConfirmation,
        IRepository<Room> roomRepo
        )
    {
        _bookingRepo = repository;
        _errorHandler = errorHandler;
        _promptForId = promptForId;
        _updateConfirmation = updateConfirmation;
        _roomRepo = roomRepo;

    }
    public void Delete()
    {
        while (true)
        {
            try
            {
                var id = _promptForId.GetValidId("hard delete", "booking");
                var currentBooking = _bookingRepo.GetItemById(id);
                if (currentBooking == null) continue;

                if (!currentBooking.IsPaid)
                {
                    _errorHandler.DisplayError("Invoice not paid." +
                        "\nThe invoice must be paid to proceed with this action.");
                    continue;
                }

                
                var room = _roomRepo.GetItemById(currentBooking.RoomId);
                room.IsActive = true;
                _roomRepo.Update(room);
                _bookingRepo.RemoveItemById(id);

                _updateConfirmation.Confirmation($"Booking with ID: {currentBooking.Id}. " +
                    $"\nHas been deleted successfully.");
                break;
            }
            catch (Exception)
            {
                _errorHandler.DisplayError("Invalid input. Try again.");
            }
        }


    }
}

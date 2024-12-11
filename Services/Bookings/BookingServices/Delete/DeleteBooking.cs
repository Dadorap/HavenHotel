using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;
using HavenHotel.Models;

namespace HavenHotel.Services.BookingServices.Services.Delete;

public class DeleteBooking : IDelete
{
    private readonly IHardDeleteItem _hardDeleteItem;
    private readonly IRepository<Booking> _bookingRepo;

    public DeleteBooking(IHardDeleteItem hardDeleteItem, IRepository<Booking> repository)
    {
        _hardDeleteItem = hardDeleteItem;
        _bookingRepo = repository;
    }
    public void Delete()
    {

        _hardDeleteItem.HardDelete("booking");

    }
}

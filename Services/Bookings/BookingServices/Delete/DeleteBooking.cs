using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;

namespace HavenHotel.Services.BookingServices.Services.Delete;

public class DeleteBooking : IDelete
{
    private readonly IHardDeleteItem _hardDeleteItem;

    public DeleteBooking(IHardDeleteItem hardDeleteItem)
    {
        _hardDeleteItem = hardDeleteItem;
    }
    public void Delete()
    {
        _hardDeleteItem.HardDelete("booking");

    }
}

using HavenHotel.Interfaces.DeleteInterfaces;

namespace HavenHotel.Controllers.BookingController.Delete;

public class UnDeleteBooking : IUnDelete
{
    private readonly IUnDeleteItem _unDeleteItem;
    public UnDeleteBooking(IUnDeleteItem unDeleteItem)
    {
        _unDeleteItem = unDeleteItem;
    }
    public void UndoDete()
    {
        _unDeleteItem.UnDelete("BOOKING");
    }
}

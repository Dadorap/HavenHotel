using HavenHotel.Interfaces.DeleteInterfaces;

namespace HavenHotel.Controllers.GuestController.Delete;

public class UnDeleteGuest : IUnDelete
{
    private readonly IUnDeleteItem _unDeleteItem;
    public UnDeleteGuest(IUnDeleteItem unDeleteItem)
    {
        _unDeleteItem = unDeleteItem;
    }
    public void UndoDete()
    {
        _unDeleteItem.UnDelete("GUEST");
    }
}

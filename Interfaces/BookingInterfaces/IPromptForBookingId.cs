namespace HavenHotel.Interfaces.BookingInterfaces;

public interface IPromptForBookingId
{
    int GetValidBookingId(string text, string whoCalling);
}
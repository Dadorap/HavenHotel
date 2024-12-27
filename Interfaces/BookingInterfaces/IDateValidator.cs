namespace HavenHotel.Interfaces.BookingInterfaces;

public interface IDateValidator
{
    bool IsCorrectStartDate(DateOnly dateOnly);
}
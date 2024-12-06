namespace HavenHotel.Bookings;

public interface IDateValidator
{
    bool IsCorrectStartDate(DateOnly dateOnly);

     bool IsCorrectEndDate(DateOnly startDate, DateOnly endDate);
}
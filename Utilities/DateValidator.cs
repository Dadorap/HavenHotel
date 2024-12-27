using HavenHotel.Interfaces.BookingInterfaces;

namespace HavenHotel.Utilities;

public class DateValidator : IDateValidator
{
    public bool IsCorrectStartDate(DateOnly dateOnly)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        bool isCorrectDate = dateOnly >= today;

        return isCorrectDate;
    }

}

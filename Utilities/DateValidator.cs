using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HavenHotel.Interfaces.BookingInterfaces;

namespace HavenHotel.Utilities
{
    public class DateValidator : IDateValidator
    {
        public bool IsCorrectStartDate(DateOnly dateOnly)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            bool isCorrectDate = dateOnly >= today;

            return isCorrectDate;
        }
        public bool IsCorrectEndDate(DateOnly startDate, DateOnly endDate)
        {
            bool isCorrectDate = endDate >= startDate;

            return isCorrectDate;
        }
    }
}

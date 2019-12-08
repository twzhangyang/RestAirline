using System;

namespace RestAirline.Booking.Domain.Booking.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsGreatThanNow(this DateTime @this)
        {
            return @this.ToUniversalTime() > DateTime.Now.ToUniversalTime();
        }
    }
}
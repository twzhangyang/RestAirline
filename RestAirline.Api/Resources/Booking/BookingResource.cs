using System;

namespace RestAirline.Api.Resources.Booking
{
    public class BookingResource
    {
        [Obsolete("For serialization")]
        public BookingResource() { }

        public BookingResource(Domain.Booking.Booking booking)
        {

        }
    }
}
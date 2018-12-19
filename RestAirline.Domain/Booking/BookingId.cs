using EventFlow.Core;

namespace RestAirline.Domain.Booking
{
    public class BookingId : Identity<BookingId>
    {
        public BookingId(string value) : base(value)
        {
        }
    }
}
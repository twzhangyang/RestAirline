using EventFlow.Aggregates;

namespace RestAirline.Domain.Booking
{
    public class Booking: AggregateRoot<Booking, BookingId>
    {
        public Booking(BookingId id) : base(id)
        {
            
        }
    }
}
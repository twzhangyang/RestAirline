using EventFlow.Aggregates;

namespace RestAirline.Domain.Booking
{
    public class Booking: AggregateRoot<Booking, BookingId>
    {
        private readonly BookingState _state = new BookingState();
        
        public Booking(BookingId id) : base(id)
        {
            Register(_state);
        }
    }
}
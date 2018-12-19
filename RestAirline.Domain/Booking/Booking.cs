using System.Collections.Generic;
using EventFlow.Aggregates;
using RestAirline.Domain.Booking.Trip;

namespace RestAirline.Domain.Booking
{
    public class Booking: AggregateRoot<Booking, BookingId>
    {
        private readonly BookingState _state = new BookingState();
        
        public Booking(BookingId id) : base(id)
        {
            Register(_state);
        }

        public IReadOnlyList<Journey> Journeys => _state.Journeys;
        
    }
}
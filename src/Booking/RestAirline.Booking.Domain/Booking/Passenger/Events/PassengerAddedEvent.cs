using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace RestAirline.Booking.Domain.Booking.Passenger.Events
{
    [EventVersion("PassengerAdded", 1)]
    public class PassengerAddedEvent : AggregateEvent<Booking, BookingId>
    {
        public Passenger Passenger { get; }

        public PassengerAddedEvent(Passenger passenger)
        {
            Passenger = passenger;
        }
    }
}
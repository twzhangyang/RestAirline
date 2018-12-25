using System.Collections.Generic;
using EventFlow.Aggregates;
using RestAirline.Domain.Booking.Events;
using RestAirline.Domain.Booking.Trip;
using RestAirline.Domain.Booking.Trip.Events;

namespace RestAirline.Domain.Booking
{
    public class BookingState : AggregateState<Booking, BookingId, BookingState>,
        IApply<JourneysSelectedEvent>,
        IApply<PassengerAddedEvent>
    {
        public IReadOnlyList<Journey> Journeys => _journeys.AsReadOnly();

        public IReadOnlyList<Passenger> Passengers => _passengers.AsReadOnly();

        private List<Journey> _journeys;

        private readonly List<Passenger> _passengers;

        public BookingState()
        {
            _passengers = new List<Passenger>();
            _journeys = new List<Journey>();
        }

        public void Apply(JourneysSelectedEvent aggregateEvent)
        {
            _journeys = aggregateEvent.Journeys;
        }


        public void Apply(PassengerAddedEvent aggregateEvent)
        {
            _passengers.Add(aggregateEvent.Passenger);
        }
    }
}
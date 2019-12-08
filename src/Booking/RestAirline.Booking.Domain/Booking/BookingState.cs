using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using RestAirline.Booking.Domain.Booking.Passenger.Events;
using RestAirline.Booking.Domain.Booking.Snapshots;
using RestAirline.Booking.Domain.Booking.Trip;
using RestAirline.Booking.Domain.Booking.Trip.Events;

namespace RestAirline.Booking.Domain.Booking
{
    public class BookingState : AggregateState<Booking, BookingId, BookingState>,
        IApply<JourneysSelectedEvent>,
        IApply<PassengerAddedEvent>,
        IApply<PassengerNameUpdatedEvent>
    {
        public IReadOnlyList<Journey> Journeys => _journeys.AsReadOnly();

        public IReadOnlyList<Passenger.Passenger> Passengers => _passengers.AsReadOnly();

        private List<Journey> _journeys;

        private List<Passenger.Passenger> _passengers;

        public BookingState()
        {
            _passengers = new List<Passenger.Passenger>();
            _journeys = new List<Journey>();
        }

        public void LoadSnapshot(BookingSnapshot snapshot)
        {
            _journeys = snapshot.Journeys.ToList();
            _passengers = snapshot.Passengers.ToList();
        }
        
        public void Apply(JourneysSelectedEvent aggregateEvent)
        {
            _journeys = aggregateEvent.Journeys;
        }

        public void Apply(PassengerAddedEvent aggregateEvent)
        {
            _passengers.Add(aggregateEvent.Passenger);
        }

        public void Apply(PassengerNameUpdatedEvent aggregateEvent)
        {
            var passenger = _passengers.Single(p => p.PassengerKey == aggregateEvent.PassengerKey);
            passenger.UpdateName(aggregateEvent.Name);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Extensions;
using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;
using RestAirline.Booking.Domain.Booking.Passenger;
using RestAirline.Booking.Domain.Booking.Passenger.Events;
using RestAirline.Booking.Domain.Booking.Snapshots;
using RestAirline.Booking.Domain.Booking.Specs;
using RestAirline.Booking.Domain.Booking.Trip;
using RestAirline.Booking.Domain.Booking.Trip.Events;
using RestAirline.Booking.Domain.Booking.Exceptions;
using RestAirline.Booking.Domain.Booking.Extensions;

namespace RestAirline.Booking.Domain.Booking
{
    public class Booking : SnapshotAggregateRoot<Booking, BookingId, BookingSnapshot>
    {
        private readonly BookingState _state = new BookingState();

        private const int SnapshotEveryVersion = 2;

        public Booking(BookingId id) : base(id, SnapshotEveryFewVersionsStrategy.With(SnapshotEveryVersion))
        {
            Register(_state);
        }

        public IReadOnlyList<Journey> Journeys => _state.Journeys;

        public IReadOnlyList<Passenger.Passenger> Passengers => _state.Passengers;

        public void SelectJourneys(List<Journey> journeys)
        {
            if (journeys == null)
            {
                throw new ArgumentNullException($"{nameof(journeys)} is null");
            }

            AggregateIsNewSpecification.Create().ThrowDomainErrorIfNotSatisfied(this);

            JourneyValidationSpecification.Create().ThrowDomainErrorIfNotSatisfied(journeys);

            // Raise event
            Emit(new JourneysSelectedEvent(journeys));
        }

        public void AddPassenger(Passenger.Passenger passenger)
        {
            if (passenger == null)
            {
                throw new ArgumentNullException($"{nameof(passenger)} is null");
            }

            new PassengerValidationSpecification(Passengers).ThrowDomainErrorIfNotSatisfied(passenger);

            Emit(new PassengerAddedEvent(passenger));
        }

        public void UpdatePassengerName(string passengerKey, string name)
        {
            var nameSpec = new PassengerNameValidationSpecification(name);
            var updateSpec = new PassengerNameUpdateSpecification(passengerKey, name);
            nameSpec.And(updateSpec).ThrowDomainErrorIfNotSatisfied(Passengers);

            var passenger = Passengers.Single(x => x.PassengerKey == passengerKey);
            passenger.UpdateName(name);

            Emit(new PassengerNameUpdatedEvent(passengerKey, name));
        }

        protected override Task<BookingSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
        {
            var snapshot = new BookingSnapshot(Journeys, Passengers);

            return Task.FromResult(snapshot);
        }

        protected override Task LoadSnapshotAsync(BookingSnapshot snapshot, ISnapshotMetadata metadata,
            CancellationToken cancellationToken)
        {
            _state.LoadSnapshot(snapshot);
            
            return Task.CompletedTask;
        }
    }
}
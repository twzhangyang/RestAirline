using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.Extensions;
using RestAirline.Domain.Booking.Events;
using RestAirline.Domain.Booking.Exceptions;
using RestAirline.Domain.Booking.Extensions;
using RestAirline.Domain.Booking.Specs;
using RestAirline.Domain.Booking.Trip;
using RestAirline.Domain.Booking.Trip.Events;

namespace RestAirline.Domain.Booking
{
    public class Booking : AggregateRoot<Booking, BookingId>
    {
        private readonly BookingState _state = new BookingState();

        public Booking(BookingId id) : base(id)
        {
            Register(_state);
        }

        public IReadOnlyList<Journey> Journeys => _state.Journeys;

        public IReadOnlyList<Passenger> Passengers => _state.Passengers;

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

        public void AddPassenger(Passenger passenger)
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.Extensions;
using RestAirline.Domain.Booking.Events;
using RestAirline.Domain.Booking.Exceptions;
using RestAirline.Domain.Booking.Extensions;
using RestAirline.Domain.Booking.Trip;
using RestAirline.Domain.Booking.Trip.Events;

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

        public IReadOnlyList<Passenger> Passengers => _state.Passengers;

        public void SelectJourneys(List<Journey> journeys)
        {
            //1. Argument validation. keep domain state is valid
            if (journeys == null)
            {
                throw new ArgumentNullException($"{nameof(journeys)} is null");
            }

            if (!journeys.Any())
            {
                throw new ArgumentException($"{nameof(journeys)} is empty");
            }

            //2. Business rules
            if (!IsNew)
            {
                throw new AggregateIsNotNewException(this);
            }

            foreach (var journey in journeys)
            {
                if (!journey.DepartureDate.IsGreatThanNow())
                {
                    throw new DepartureDateTimeIsLessThanNowException(journey);
                }
            }
            
            //3. Raise event
            Emit(new JourneysSelectedEvent(journeys));
        }
        
        public void AddPassenger(Passenger passenger)
        {
            if (passenger == null)
            {
                throw new ArgumentNullException($"{nameof(passenger)} is null");
            }
            
            new PassengerIsValidSpecification(Passengers).ThrowDomainErrorIfNotSatisfied(passenger);
            
            Emit(new PassengerAddedEvent(passenger));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using RestAirline.Domain.Booking.DomainEvents;
using RestAirline.Domain.Booking.Exceptions;
using RestAirline.Domain.Booking.Extensions;
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
    }
}
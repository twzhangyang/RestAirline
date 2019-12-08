using System.Collections.Generic;
using EventFlow.Commands;
using RestAirline.Booking.Domain.Booking;

namespace RestAirline.Booking.Commands.Journey
{
    public class SelectJourneysCommand : Command<Domain.Booking.Booking, BookingId>
    {
        public List<Domain.Booking.Trip.Journey> Journeys { get; }

        public SelectJourneysCommand(BookingId aggregateId, List<Domain.Booking.Trip.Journey> journeys) : base(aggregateId)
        {
            Journeys = journeys;
        }
    }
}
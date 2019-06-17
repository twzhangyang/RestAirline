using System.Collections.Generic;
using EventFlow.Commands;
using RestAirline.Domain.Booking;

namespace RestAirline.Commands.Journey
{
    public class SelectJourneysCommand : Command<Booking, BookingId>
    {
        public List<Domain.Booking.Trip.Journey> Journeys { get; }

        public SelectJourneysCommand(BookingId aggregateId, List<Domain.Booking.Trip.Journey> journeys) : base(aggregateId)
        {
            Journeys = journeys;
        }
    }
}
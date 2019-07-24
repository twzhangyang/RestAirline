using System.Collections.Generic;
using System.Linq;
using EventFlow.Snapshots;
using RestAirline.Domain.Booking.Trip;

namespace RestAirline.Domain.Booking.Snapshots
{
    [SnapshotVersion("booking", 1)]
    public class BookingSnapshot : ISnapshot
    {
        public IReadOnlyCollection<Journey> Journeys { get; private set; }

        public IReadOnlyCollection<Passenger> Passengers { get; private set; }

        public BookingSnapshot()
        {
            Journeys = new List<Journey>();
            Passengers = new List<Passenger>();
        }

        public BookingSnapshot(IEnumerable<Journey> journeys, IEnumerable<Passenger> passengers)
        {
            Journeys = (journeys ?? Enumerable.Empty<Journey>()).ToList();
            Passengers = (passengers ?? Enumerable.Empty<Passenger>()).ToList();
        }
    }
}
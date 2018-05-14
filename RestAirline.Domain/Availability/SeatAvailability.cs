using System.Collections.Generic;
using System.Linq;
using RestAirline.Domain.Shared;

namespace RestAirline.Domain.Availability
{
    public class SeatAvailability
    {
        public List<Seat> GetAvailableSeats(Flight flight)
        {
            return Enumerable.Range(1, 10).Select(CreateSeat).ToList();
        }

        private Seat CreateSeat(int number)
        {
            return new Seat()
            {
                SeatType = SeatType.Business,
                Number = $"C{number}"
            };
        }
    }
}
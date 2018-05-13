using System;
using RestAirline.Api.Domain.Shared;

namespace RestAirline.Api.Domain.Booking.Checkin
{
    public class Checkin
    {
        public Checkin(Passenger passenger, Trip.Journey journey)
        {
            Passenger = passenger;
            Journey = journey;

            CheckinTime=DateTime.Now;
        }

        public Passenger Passenger { get; private set; }

        public Trip.Journey Journey { get; private set; }

        public DateTime CheckinTime { get; private set; }
    }
}
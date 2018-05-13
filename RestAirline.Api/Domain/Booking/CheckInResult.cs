using RestAirline.Api.Domain.Shared;

namespace RestAirline.Api.Domain.Booking
{
    public class CheckInResult
    {
        public CheckInResult(Passenger passenger, Trip.Journey journey)
        {
            Passenger = passenger;
            Journey = journey;
        }

        public Passenger Passenger { get; private set; }

        public Trip.Journey Journey { get; private set; }
    }
}
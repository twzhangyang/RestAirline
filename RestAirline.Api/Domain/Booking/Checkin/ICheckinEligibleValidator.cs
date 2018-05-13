using RestAirline.Api.Domain.Shared;

namespace RestAirline.Api.Domain.Booking.Checkin
{
    public interface ICheckinEligibleValidator
    {
        bool IsEligible(Passenger passenger, Trip.Journey journey);
    }
}
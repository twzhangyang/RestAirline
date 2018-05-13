using RestAirline.Domain.Shared;

namespace RestAirline.Domain.Booking.Checkin
{
    public interface ICheckinEligibleValidator
    {
        bool IsEligible(Passenger passenger, Trip.Journey journey);
    }
}
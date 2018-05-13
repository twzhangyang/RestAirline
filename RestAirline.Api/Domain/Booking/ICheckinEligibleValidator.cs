using RestAirline.Api.Domain.Shared;

namespace RestAirline.Api.Domain.Booking
{
    public interface ICheckinEligibleValidator
    {
        bool IsEligible(Passenger passenger, Trip.Journey journey);
    }
}
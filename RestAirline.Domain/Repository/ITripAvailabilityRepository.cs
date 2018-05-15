using RestAirline.Domain.Availability;

namespace RestAirline.Domain.Repository
{
    public interface ITripAvailabilityRepository
    {
        TripAvailability Get();
    }
}
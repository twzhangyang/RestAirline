using RestAirline.Api.Domain.Shared;

namespace RestAirline.Api.Domain.Booking
{
    public interface ISeatAllocator
    {
        Seat AllocateSeat();
    }
}
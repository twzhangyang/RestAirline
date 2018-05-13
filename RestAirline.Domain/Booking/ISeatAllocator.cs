using RestAirline.Domain.Shared;

namespace RestAirline.Domain.Booking
{
    public interface ISeatAllocator
    {
        Seat AllocateSeat();
    }
}
using System;

namespace RestAirline.Domain.Repository
{
    public interface IBookingRepository
    {
        Booking.Booking Get(Guid id);
    }
}
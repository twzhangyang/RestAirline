using System;
using EventFlow.Queries;
using RestAirline.Booking.ReadModel.EntityFramework;

namespace RestAirline.Booking.Queries.EntityFramework.Booking
{
    public class BookingIdQuery : IQuery<BookingReadModel>
    {
        public string BookingId { get; }

        public BookingIdQuery(string bookingId)
        {
            if (string.IsNullOrEmpty(bookingId))
            {
                throw new ArgumentNullException("bookingId is null");
            }
            
            BookingId = bookingId;
        }
    }
}
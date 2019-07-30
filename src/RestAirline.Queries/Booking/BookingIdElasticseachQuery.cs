using System;
using EventFlow.Queries;
using RestAirline.ReadModel.EntityFramework;

namespace RestAirline.Queries.Booking
{
    public class BookingIdElasticsearchQuery : IQuery<BookingReadModel>
    {
        public string BookingId { get; }

        public BookingIdElasticsearchQuery(string bookingId)
        {
            if (string.IsNullOrEmpty(bookingId))
            {
                throw new ArgumentNullException("bookingId is null");
            }
            
            BookingId = bookingId;
        }
    }
}
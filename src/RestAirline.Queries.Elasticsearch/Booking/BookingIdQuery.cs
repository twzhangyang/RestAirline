using System;
using EventFlow.Queries;
using RestAirline.ReadModel.Elasticsearch;

namespace RestAirline.Queries.Elasticsearch.Booking
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
using System;
using EventFlow.Queries;
using RestAirline.ReadModel.MongoDb;

namespace RestAirline.Queries.MongoDB.Booking
{
    public class BookingIdQuery : IQuery<MongoDbBookingReadModel>
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
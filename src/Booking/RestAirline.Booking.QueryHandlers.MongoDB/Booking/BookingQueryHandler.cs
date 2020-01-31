using System.Threading;
using System.Threading.Tasks;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using MongoDB.Driver;
using RestAirline.Booking.Queries.MongoDB.Booking;
using RestAirline.ReadModel.MongoDb;

namespace RestAirline.Booking.QueryHandlers.MongoDB.Booking
{
    public class BookingQueryHandler : IQueryHandler<BookingIdQuery, BookingReadModel>
    {
        private readonly IMongoDbReadModelStore<BookingReadModel> _readModelStore;

        public BookingQueryHandler(IMongoDbReadModelStore<BookingReadModel> readModelStore)
        {
            _readModelStore = readModelStore;
        }
        
        public async Task<BookingReadModel> ExecuteQueryAsync(BookingIdQuery query, CancellationToken cancellationToken)
        {
            var bookingCursor = await _readModelStore.FindAsync(f => f.Id == query.BookingId, cancellationToken: cancellationToken);
            var booking = await bookingCursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return booking;
        }
    }
}
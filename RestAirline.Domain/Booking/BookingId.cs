using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace RestAirline.Domain.Booking
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class BookingId : Identity<BookingId>
    {
        public BookingId(string value) : base(value)
        {
        }
    }
}
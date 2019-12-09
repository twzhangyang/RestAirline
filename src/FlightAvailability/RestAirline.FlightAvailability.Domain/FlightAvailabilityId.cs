using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace RestAirline.FlightAvailability.Domain
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class FlightAvailabilityId: Identity<FlightAvailabilityId>
    {
        public FlightAvailabilityId(string value) : base(value)
        {
        }
    }
}
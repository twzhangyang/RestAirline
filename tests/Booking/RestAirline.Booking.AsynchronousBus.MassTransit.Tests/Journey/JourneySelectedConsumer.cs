using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using RestAirline.Booking.Domain.Booking.Trip.Events;

namespace RestAirline.Booking.AsynchronousBus.MassTransit.Tests.Journey
{
    public class JourneySelectedConsumer : IConsumer<JourneysSelectedEvent>
    {
        public static string Origin { get; private set; }
        
        public static string Destination { get; private set; }
        
        public Task Consume(ConsumeContext<JourneysSelectedEvent> context)
        {
            var journey = context.Message.Journeys.First();
            Origin = journey.DepartureStation;
            Destination = journey.ArriveStation;
            
            return Task.CompletedTask;
        }
    }
}
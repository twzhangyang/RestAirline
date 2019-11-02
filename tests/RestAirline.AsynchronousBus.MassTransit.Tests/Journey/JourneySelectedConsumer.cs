using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using RestAirline.Domain.Booking.Trip.Events;

namespace RestAirline.AsynchronousBus.MassTransit.Tests.Journey
{
    public class JourneySelectedConsumer : IConsumer<JourneysSelectedEvent>
    {
        public string Origin { get; private set; }
        
        public string Destination { get; private set; }
        
        public Task Consume(ConsumeContext<JourneysSelectedEvent> context)
        {
            var journey = context.Message.Journeys.First();
            Origin = journey.DepartureStation;
            Destination = journey.ArriveStation;
            
            return Task.CompletedTask;
        }
    }
}
using System.Reflection;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using EventFlow.Extensions;
using RestAirline.Booking.Domain.EventSourcing;

namespace RestAirline.Booking.Domain
{
    public class BookingDomainModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .AddDefaults(typeof(BookingDomainModule).Assembly);
        }
    }
}
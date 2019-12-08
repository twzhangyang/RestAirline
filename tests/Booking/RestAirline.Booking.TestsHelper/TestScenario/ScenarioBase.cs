using System.Threading.Tasks;
using EventFlow;
using RestAirline.Booking.Domain.Booking;

namespace RestAirline.TestsHelper.TestScenario
{
    public abstract class ScenarioBase
    {
        protected ICommandBus CommandBus { get;  set; }

        protected ScenarioBase(ICommandBus commandBus)
        {
            CommandBus = commandBus;
        }

        public BookingId BookingId { get; protected set; }

        public abstract Task Execute();
    }
}
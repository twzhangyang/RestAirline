using System.Threading.Tasks;
using EventFlow;

namespace RestAirline.Shared.Testing
{
    public abstract class ScenarioBase<TAggregateId>
    {
        protected ICommandBus CommandBus { get;  set; }

        protected ScenarioBase(ICommandBus commandBus)
        {
            CommandBus = commandBus;
        }

        public TAggregateId Id { get; protected set; }

        public abstract Task Execute();
    }
}
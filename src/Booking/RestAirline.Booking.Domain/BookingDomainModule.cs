using Autofac;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

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

    public interface IDog
    {
        string SayHello();
    }

    public class Dog : IDog
    {
        public string SayHello()
        {
            return "Hello";
        }
    }

    public class AutoFactModuleA : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Dog>().As<IDog>().InstancePerRequest();
        }
    }
}
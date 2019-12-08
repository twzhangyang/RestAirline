using System.Reflection;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using EventFlow.Extensions;
using RestAirline.Booking.ReadModel.EntityFramework.DBContext;

namespace RestAirline.Booking.ReadModel.EntityFramework
{
    public class EntityFrameworkReadModelModule: IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.ConfigureEntityFramework(EntityFrameworkConfiguration.New)
                .AddDefaults(typeof(EntityFrameworkReadModelModule).Assembly)
                .AddDbContextProvider<RestAirlineReadModelContext, RestAirlineReadModelDbContextProvider>()
                .UseEntityFrameworkReadModel<BookingReadModel, RestAirlineReadModelContext>()
                ;
        }
    }
}
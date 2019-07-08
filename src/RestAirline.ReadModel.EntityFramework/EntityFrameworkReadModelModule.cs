using System.Reflection;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using EventFlow.Extensions;
using RestAirline.ReadModel.EntityFramework.DBContext;

namespace RestAirline.ReadModel.EntityFramework
{
    public class EntityFrameworkReadModelModule: IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.ConfigureEntityFramework(EntityFrameworkConfiguration.New)
                .AddDefaults(typeof(EntityFrameworkReadModelModule).Assembly)
                .AddDbContextProvider<ReadModelContext, ReadModelDbContextProvider>()
                .UseEntityFrameworkReadModel<BookingReadModel, ReadModelContext>()
                ;
        }
    }
}
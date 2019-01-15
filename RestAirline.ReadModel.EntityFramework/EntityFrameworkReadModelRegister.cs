using System.Reflection;
using EventFlow;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using EventFlow.Extensions;

namespace RestAirline.ReadModel.EntityFramework
{
    public static class EntityFrameworkReadModelRegister
    {
        public static Assembly Assembly { get; } = typeof(EntityFrameworkReadModelRegister).Assembly;
        
        public static IEventFlowOptions ConfigureEntityFrameworkReadModel(this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                .AddDefaults(Assembly)
                .UseEntityFrameworkReadModel<BookingReadModel, ReadModelDbContext>()
                .ConfigureEntityFramework(EntityFrameworkConfiguration.New)
                .AddDbContextProvider<ReadModelDbContext, ReadModelDbContextProvider>();

        }
    }
}
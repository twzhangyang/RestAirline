using System.Reflection;
using EventFlow;
using EventFlow.Extensions;
using EventFlow.MsSql.Extensions;
using RestAirline.ReadModel.EntityFramework;

namespace RestAirline.ReadModel.MsSql
{
    public static class MsSqlReadModelRegister
    {
        public static Assembly Assembly { get; } = typeof(MsSqlReadModelRegister).Assembly;
        
        public static IEventFlowOptions ConfigureMsSqlReadModel(this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                .AddDefaults(Assembly)
                .UseMssqlReadModel<BookingReadModel>()
                .UseMssqlReadModel<StationsReadModel>()
                ;

        }
    }
}
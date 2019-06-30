using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.Core;
using RestAirline.Domain.Booking;

namespace RestAirline.Domain.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly IRootResolver Resolver;
        protected readonly IAggregateStore AggregateStore;
        protected readonly BookingId BookingId;

        public TestBase()
        {
            BookingId = BookingId.New;
            Resolver = EventFlowOptions.New
                .RegisterModule<BookingModule>()
                .CreateResolver();

            AggregateStore = Resolver.Resolve<IAggregateStore>();
        }

        protected async Task UpdateAsync<TAggregate, TIdentity>(TIdentity id, Action<TAggregate> action)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
        {
            await AggregateStore.UpdateAsync<TAggregate, TIdentity>(
                    id,
                    SourceId.New,
                    (a, c) =>
                    {
                        action(a);
                        return Task.FromResult(0);
                    },
                    CancellationToken.None)
                .ConfigureAwait(false);
        }

        public void Dispose()
        {
            Resolver?.Dispose();
        }
    }
}
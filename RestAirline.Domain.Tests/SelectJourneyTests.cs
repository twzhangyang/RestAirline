using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.Core;
using EventFlow.Extensions;
using EventFlow.Logs;
using FluentAssertions;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Trip;
using RestAirline.TestsHelper;
using Xunit;

namespace RestAirline.Domain.Tests
{
    public class SelectJourneyTests : IDisposable
    {
        private readonly IRootResolver _resolver;
        private readonly IAggregateStore _aggregateStore;
        private List<Journey> _journeys;
        private readonly BookingId _bookingId;

        public SelectJourneyTests()
        {
            _bookingId = BookingId.New;
            _resolver = EventFlowOptions.New
                .ConfigureBookingDomain()
                .CreateResolver();

            _aggregateStore = _resolver.Resolve<IAggregateStore>();
        }

        [Fact]
        public async Task AfterSelectedJourneysShouldHaveJourneys()
        {
            //Arrange
            _journeys = new JourneysBuilder().BuildJourneys();
            void SelectJourney(Booking.Booking b) => b.SelectJourneys(_journeys);

            //Act
            await UpdateAsync(_bookingId, (Action<Booking.Booking>) SelectJourney);
            
            //Assert
            var booking =await _aggregateStore.LoadAsync<Booking.Booking,BookingId>(_bookingId,CancellationToken.None);
            booking.Journeys.Should().NotBeEmpty();
        }

        private async Task UpdateAsync<TAggregate, TIdentity>(TIdentity id, Action<TAggregate> action)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
        {
            await _aggregateStore.UpdateAsync<TAggregate, TIdentity>(
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
            _resolver?.DisposeSafe(new ConsoleLog(), "");
        }
    }
}
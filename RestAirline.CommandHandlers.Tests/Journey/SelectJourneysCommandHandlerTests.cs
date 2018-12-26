using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using FluentAssertions;
using RestAirline.Commands.Journey;
using RestAirline.Domain;
using RestAirline.Domain.Booking;
using RestAirline.TestsHelper;
using Xunit;

namespace RestAirline.CommandHandlers.Tests.Journey
{
    public class SelectJourneysCommandHandlerTests : IDisposable
    {
        private readonly IRootResolver _resolver;
        private readonly ICommandBus _commandBus;
        private readonly BookingId _bookingId;
        private readonly IAggregateStore _aggregateStore;


        public SelectJourneysCommandHandlerTests()
        {
            _bookingId = BookingId.New;
            _resolver = EventFlowOptions.New
                .ConfigureBookingDomain()
                .ConfigureBookingCommands()
                .ConfigureBookingCommandHandlers()
                .CreateResolver();

            _commandBus = _resolver.Resolve<ICommandBus>();
            _aggregateStore = _resolver.Resolve<IAggregateStore>();
        }

        [Fact]
        public async Task WhenSendSelectJourneysCommandShouldAddJourneys()
        {
            //Arrange
            var journeys = new JourneysBuilder().BuildJourneys();
            var selectJourneysCommand = new SelectJourneysCommand(_bookingId, journeys);

            //Act
            await _commandBus.PublishAsync(selectJourneysCommand, CancellationToken.None);

            //Assert
            var booking = await _aggregateStore.LoadAsync<Booking, BookingId>(_bookingId, CancellationToken.None);
            booking.Journeys.Should().NotBeEmpty();
        }

        public void Dispose()
        {
            _resolver?.Dispose();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using FluentAssertions;
using RestAirline.Commands.Journey;
using RestAirline.Domain.Booking;
using RestAirline.Queries.Journeys;
using RestAirline.ReadModel.Tests;
using RestAirline.TestsHelper;
using Xunit;

namespace RestAirline.QueryHandlers.Tests.Journeys
{
    public class DepartureStationQueryHandlerTests : TestBase
    {
        private readonly IQueryHandler<GetDepartureStationsQuery, List<string>> _queryHandler;

        public DepartureStationQueryHandlerTests()
        {
            _queryHandler = Resolver.Resolve<IQueryHandler<GetDepartureStationsQuery, List<string>>>();
        }
        
        [Fact]
        public async Task  ShouldGetDepartureStation()
        {
            //Arrange
            var journeys = new JourneysBuilder().BuildJourneys();
            var selectJourneysCommand = new SelectJourneysCommand(BookingId.New, journeys);
            await CommandBus.PublishAsync(selectJourneysCommand, CancellationToken.None);

            //Act
            var query = new GetDepartureStationsQuery(DateTime.Today, DateTime.Today.AddDays(3));
            var stations = await _queryHandler.ExecuteQueryAsync(query, CancellationToken.None);

            //Assert
            stations.Should().NotBeEmpty();
        }
    }
}
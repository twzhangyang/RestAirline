using System;
using System.Collections.Generic;
using EventFlow.Queries;

namespace RestAirline.Queries.Journeys
{
    public class GetDepartureStationsQuery : IQuery<List<string>>
    {
        public DateTime DepartureTime1 { get; }
        public DateTime DepartureTime2 { get; }

        public GetDepartureStationsQuery(DateTime departureTime1, DateTime departureTime2)
        {
            DepartureTime1 = departureTime1;
            DepartureTime2 = departureTime2;
        }
    }
}
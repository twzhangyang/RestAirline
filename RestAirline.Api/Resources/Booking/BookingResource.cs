using System;
using System.Collections.Generic;
using RestAirline.ReadModel;
using RestAirline.ReadModel.Booking;

namespace RestAirline.Api.Resources.Booking
{
    public class BookingResource
    {
        [Obsolete("For serialization")]
        public BookingResource()
        {
        }

        public BookingResource(BookingReadModel booking)
        {
            Id = booking.Id.Value;
            Journeys = booking.Journeys;
        }

        public string Id { get; set; }

        public List<Journey> Journeys { get; private set; }
    }
}
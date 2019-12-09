namespace RestAirline.FlightAvailability.Domain.Events
{
    public static class FlightAddedEventMapper
    {
        public static Flight ToModel(this FlightAddedEvent @event)
        {
            return new Flight
            {
                Aircraft = @event.Aircraft,
                Number = @event.Number,
                Price = @event.Price,
                ArriveDate = @event.ArriveDate,
                ArriveStation = @event.ArriveStation,
                DepartureDate = @event.DepartureDate,
                DepartureStation = @event.DepartureStation,
                FlightKey = @event.FlightKey
            };
        }
    }
}
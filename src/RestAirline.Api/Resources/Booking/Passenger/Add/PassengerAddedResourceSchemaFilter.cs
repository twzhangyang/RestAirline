using System;
using RestAirline.Api.Hypermedia;
using RestAirline.Api.Resources.Booking.Passenger.Update;
using RestAirline.Domain.Booking;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RestAirline.Api.Resources.Booking.Passenger.Add
{
    public class PassengerAddedResourceSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            var bookingId = Guid.NewGuid().ToString();

            schema.Example = new PassengerAddedResource
            {
                ResourceLinks = new PassengerAddedResource.Links
                {
                    Home = new Link<RestAirlineHomeResource>("api/home"),
                    Booking = new Link<BookingResource>("api/{bookingId}")
                },
                ResourceCommands = new PassengerAddedResource.Commands
                {
                    AddPassengerCommand = new AddPassengerCommand
                    {
                        Age = 23,
                        Name = "Test",
                        Email = "test@test.com",
                        BookingId = bookingId,
                        PassengerType = PassengerType.Male,
                        PostUrl = new Link<PassengerAddedResource>("api/{bookingId}/passenger")
                    },
                    UpdatePassengerNameCommand = new UpdatePassengerNameCommand
                    {
                        Name = "new name",
                        BookingId = bookingId,
                        PassengerKey = "1",
                        PostUrl = new Link<PassengerNameUpdatedResource>(
                            "api/{bookingId}/passenger/{passengerKey}/name")
                    }
                }
            };
        }
    }
}
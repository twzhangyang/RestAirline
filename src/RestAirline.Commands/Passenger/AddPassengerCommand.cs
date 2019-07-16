using System;
using EventFlow.Commands;
using RestAirline.Domain.Booking;

namespace RestAirline.CommandHandlers.Passenger
{
    public class AddPassengerCommand : Command<Booking, BookingId>
    {
        public Domain.Booking.Passenger Passenger { get; }

        [Obsolete("For serialization")]
        public AddPassengerCommand() : base(BookingId.Empty())
        {
        }

        public AddPassengerCommand(BookingId aggregateId, Domain.Booking.Passenger passenger) : base(aggregateId)
        {
            Passenger = passenger;
        }
    }
}
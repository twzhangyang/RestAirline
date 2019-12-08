using EventFlow.Commands;
using RestAirline.Booking.Domain.Booking;

namespace RestAirline.Booking.Commands.Passenger
{
    public class UpdatePassengerNameCommand : Command<Domain.Booking.Booking, BookingId>
    {
        public string PassengerKey { get; }
        public string Name { get; }

        public UpdatePassengerNameCommand(BookingId aggregateId, string passengerKey, string name) : base(aggregateId)
        {
            PassengerKey = passengerKey;
            Name = name;
        }
    }
}
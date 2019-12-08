using EventFlow.Commands;
using RestAirline.Domain.Booking;

namespace RestAirline.Commands.Passenger
{
    public class UpdatePassengerNameCommand : Command<Booking, BookingId>
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
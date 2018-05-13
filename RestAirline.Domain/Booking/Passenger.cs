using RestAirline.Domain.Shared;

namespace RestAirline.Domain.Booking
{
    public class Passenger
    {
        public string Name { get; set; }

        public PassengerType PassengerType { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public Maybe<Seat> SelectedSeat { get; private set; }

        public Passenger()
        {
            SelectedSeat = Maybe.None<Seat>();
        }

        public void AssignSeat(Seat seat)
        {
            SelectedSeat = Maybe.Some(seat);
        }

        public void UnassignSeat()
        {
            SelectedSeat = Maybe.None<Seat>();
        }

        public void Checkin()
        {
        }
    }

    public enum PassengerType
    {
        Male,
        Female,
        Infant,
        Unknown
    }
}
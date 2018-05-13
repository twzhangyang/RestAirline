using RestAirline.Api.Domain.Shared;

namespace RestAirline.Api.Domain.Booking
{
    public class Passenger
    {
        public string Name { get; set; }

        public PassengerType PassengerType { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public Seat SelectedSeat { get; private set; }

        public Trip Trip { get; set; }


        public void AssignSeat(Seat seat)
        {
            SelectedSeat = seat;
        }

        public void Checkin()
        {
            IsCheckin = true;
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
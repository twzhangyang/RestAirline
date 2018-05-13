using System.Collections.Generic;
using RestAirline.Api.Domain.Booking.Exceptions;
using RestAirline.Api.Domain.Shared;

namespace RestAirline.Api.Domain.Booking.Checkin
{
    public class CheckinProcess
    {
        public IReadOnlyList<Checkin> CheckinList => _checkinList.AsReadOnly();

        private readonly List<Checkin> _checkinList;

        private Booking _booking;

        private CheckinProcess(Booking booking)
        {
            _checkinList = new List<Checkin>();
            _booking = booking;
        }

        public static CheckinProcess CreateCheckinProcess(Booking booking)
        {
            var checkinProcess=new CheckinProcess(booking);

            return checkinProcess;
        }

        public void Checkin(Passenger passenger, Trip.Journey journey, ICheckinEligibleValidator checkinEligibleValidator)
        {
            //Validation in here

            var isEligible = checkinEligibleValidator.IsEligible(passenger, journey);

            if (!isEligible)
            {
                var message = $"passenger:{passenger.Name} is not eligible checkin for {journey.DepartureStation} to {journey.ArriveStation}";
                throw new PassengerNotEligibleForCheckinException(message);
            }

            var checkin = new Checkin(passenger, journey);
            _checkinList.Add(checkin);
        }

        public void SendBoardingPass(Passenger passenger, Trip.Journey journey)
        {
            // Domain logic in here
        }
    }
}
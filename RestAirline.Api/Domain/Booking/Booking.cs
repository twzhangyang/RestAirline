using System;
using System.Collections.Generic;
using System.Linq;
using RestAirline.Api.Domain.Shared;

namespace RestAirline.Api.Domain.Booking
{
    public class Booking
    {
        public Guid Id { get; }

        public IReadOnlyList<Passenger> Passengers => _passengers.AsReadOnly();

        public Trip Trip { get; }

        public IReadOnlyList<Seat> Seats => _passengers.Select(p => p.SelectedSeat).ToList().AsReadOnly();

        public Maybe<AirportTransfer> AirportTransfer { get; set; }

        public IReadOnlyList<CheckInResult> CheckInResults => _checkInResults.AsReadOnly();

        private readonly List<CheckInResult> _checkInResults;

        private readonly List<Passenger> _passengers;

        private Booking(Trip trip, List<Passenger> passengers)
        {
            Id = Guid.NewGuid();
            Trip = trip;
            _passengers = passengers;
            _checkInResults = new List<CheckInResult>();
        }

        public Booking SelectTrip(Trip trip, List<Passenger> passengers)
        {
            //Validation for trip and passengers in here

            var booking = new Booking(trip, passengers);

            return booking;
        }

        public void AssignSeat(Seat seat, Passenger passenger)
        {
            //Validation in here

            var p = _passengers.Single(s => s.Name.Equals(passenger.Name));
            p.AssignSeat(seat);
        }

        public void AssignSeatsAutomaticallyForAllPassengers(ISeatAllocator seatAllocator)
        {
            _passengers.ForEach(p => p.AssignSeat(seatAllocator.AllocateSeat()));
        }

        //Like seat, passenger probably will select bags/meals/insurance etc... for each passenger

        public void AddAirportTransferService(AirportTransfer airportTransfer)
        {
            //Validation in here

            AirportTransfer = Maybe.Some(airportTransfer);
        }

        //Like airportTransfer service, passenger probably will add some service for this booking

        public void CheckIn(Passenger passenger, Trip.Journey journey, ICheckinEligibleValidator checkinEligibleValidator)
        {
            var isEligible = checkinEligibleValidator.IsEligible(passenger, journey);

            if (!isEligible)
            {
                var message = $"passenger:{passenger.Name} is not eligible checkin for {journey.DepartureStation} to {journey.ArriveStation}";
                throw new PassengerNotEligibleForCheckinException(message);
            }

            _checkInResults.Add(new CheckInResult(passenger, journey));
        }

    }
}
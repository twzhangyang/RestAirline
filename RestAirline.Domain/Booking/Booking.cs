using System;
using System.Collections.Generic;
using System.Linq;
using RestAirline.Domain.Booking.Checkin;
using RestAirline.Domain.Booking.Exceptions;
using RestAirline.Domain.Shared;

namespace RestAirline.Domain.Booking
{
    public class Booking
    {
        public Guid Id { get; }

        public IReadOnlyList<Passenger> Passengers => _passengers.AsReadOnly();

        public Trip Trip { get; }

        public IReadOnlyList<Maybe<Seat>> Seats => _passengers.Select(p => p.SelectedSeat).ToList().AsReadOnly();

        public Maybe<AirportTransfer> AirportTransfer { get; set; }

        private readonly List<Passenger> _passengers;

        private readonly CheckinProcess _checkinProcess;

        private Booking(Trip trip, List<Passenger> passengers)
        {
            Id = Guid.NewGuid();
            _checkinProcess = CheckinProcess.CreateCheckinProcess(this);

            Trip = trip;
            _passengers = passengers;
        }

        public Booking AddTrip(Trip trip, List<Passenger> passengers)
        {
            //Validation for trip and passengers in here

            var booking = new Booking(trip, passengers);

            return booking;
        }

        public void ChangeFlight(Trip.Journey journey, Flight flight)
        {
            // Checking is it eligible for changing flight;
            var isEligible = journey.DepartureDate < DateTime.Now;

            if (!isEligible)
            {
                var message = $"Journey from {journey.DepartureStation} to {journey.ArriveStation} is ineligible for changing to flight {flight.Number}";
                throw new ChangingFlightIneligibleException(message);
            }

            Trip.ChangeFlight(journey.Id, flight);
        }

        public void AssignSeat(Seat seat, Passenger passenger)
        {
            //Validation in here

            var p = _passengers.Single(s => s.Name.Equals(passenger.Name));
            p.AssignSeat(seat);
        }

        public void UnassignSeat(Passenger passenger)
        {
            var p = _passengers.Single(s => s.Name.Equals(passenger.Name));
            if (!p.SelectedSeat.HasValue())
            {
                var message = $"There is not allocated seat for passenger {passenger.Name}";
                throw new NoAllocatedSeatForPassengerException(message);
            }

            p.UnassignSeat();
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
            _checkinProcess.Checkin(passenger, journey, checkinEligibleValidator);
        }

    }
}
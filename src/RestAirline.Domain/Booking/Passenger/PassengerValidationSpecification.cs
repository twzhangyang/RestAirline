using System.Collections.Generic;
using System.Linq;
using EventFlow.Specifications;

namespace RestAirline.Domain.Booking
{
    public class PassengerValidationSpecification : Specification<Passenger>
    {
        public IReadOnlyList<Passenger> Passengers { get; }

        public PassengerValidationSpecification(IReadOnlyList<Passenger> passengers)
        {
            Passengers = passengers;
        }

        protected override IEnumerable<string> IsNotSatisfiedBecause(Passenger passenger)
        {
            if (string.IsNullOrEmpty(passenger.Name))
            {
                yield return $"name can not be empty";
            }

            if (Passengers.Any(p => p.Name.Equals(passenger.Name)))
            {
                yield return $"There is already has a passenger with this name {passenger.Name}";
            }

            if (passenger.PassengerType == PassengerType.Infant)
            {
                if (passenger.Age > 1)
                {
                    yield return $"Infant can not be great 1 year";
                }
            }
        }
    }
}
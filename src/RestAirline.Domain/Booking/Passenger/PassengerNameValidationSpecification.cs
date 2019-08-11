using System.Collections.Generic;
using EventFlow.Specifications;

namespace RestAirline.Domain.Booking.Passenger
{
    public class PassengerNameValidationSpecification : Specification<IReadOnlyList<Passenger>>
    {
        private readonly string _name;

        public PassengerNameValidationSpecification(string name)
        {
            _name = name;
        }

        protected override IEnumerable<string> IsNotSatisfiedBecause(IReadOnlyList<Passenger> passengers)
        {
            if (string.IsNullOrEmpty(_name))
            {
                yield return $"name is empty";
            }

            //Other rules for name
        }
    }
}
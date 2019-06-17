using System.Collections.Generic;
using System.Linq;
using EventFlow.Specifications;

namespace RestAirline.Domain.Booking
{
    public class PassengerNameUpdateSpecification : Specification<IReadOnlyList<Passenger>>
    {
        private readonly string _passengerKey;
        private readonly string _name;

        public PassengerNameUpdateSpecification(string passengerKey, string name)
        {
            _passengerKey = passengerKey;
            _name = name;
        }
        
        protected override IEnumerable<string> IsNotSatisfiedBecause(IReadOnlyList<Passenger> passengers)
        {
            if(string.IsNullOrEmpty(_passengerKey))
            {
                yield return $"{_passengerKey} is empty";
            }

            var passenger = passengers.FirstOrDefault(p => p.PassengerKey == _passengerKey);
            if(passenger==null)
            {
                yield return $"can not find passenger by {_passengerKey}";
            }
        }
    }
}
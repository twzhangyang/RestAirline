using System;
using System.Collections.Generic;
using EventFlow.Specifications;
using RestAirline.Shared.Extensions;

namespace RestAirline.FlightAvailability.Domain
{
    public class FlightValidationSpecification : Specification<Flight>
    {
        public static FlightValidationSpecification Create()
        {
            return new FlightValidationSpecification();
        }
        
        protected override IEnumerable<string> IsNotSatisfiedBecause(Flight obj)
        {
            if (obj.FlightKey.IsNullOrEmpty())
            {
                yield return "flight key is empty";
            }

            if (obj.DepartureDate < DateTime.UtcNow)
            {
                yield return "Departure date can not less than today";
            }

            if (obj.ArriveDate < DateTime.UtcNow)
            {
                yield return "Arrive date can not less than today";
            }
            
            if(obj.DepartureStation.IsNullOrEmpty())
            {
                yield return "Departure station is empty";
            }

            if (obj.ArriveStation.IsNullOrEmpty())
            {
                yield return "Arrive station is empty";
            }
        }
    }
}
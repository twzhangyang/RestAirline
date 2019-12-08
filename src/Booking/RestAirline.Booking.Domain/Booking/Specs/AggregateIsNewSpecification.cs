using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.Specifications;

namespace RestAirline.Booking.Domain.Booking.Specs
{
    public class AggregateIsNewSpecification : Specification<IAggregateRoot>
    {
        public static AggregateIsNewSpecification Create()
        {
            return new AggregateIsNewSpecification();
        }
        
        protected override IEnumerable<string> IsNotSatisfiedBecause(IAggregateRoot obj)
        {
            if (!obj.IsNew)
            {
                yield return $"'{obj.Name}' with ID '{obj.GetIdentity()}' is not new";
            }
        }
    }
}
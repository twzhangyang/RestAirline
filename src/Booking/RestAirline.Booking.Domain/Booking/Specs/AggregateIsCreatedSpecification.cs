using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.Specifications;

namespace RestAirline.Booking.Domain.Booking.Specs
{
    public class AggregateIsCreatedSpecification : Specification<IAggregateRoot>
    {
        public static AggregateIsCreatedSpecification Create()
        {
            return new AggregateIsCreatedSpecification();
        }
        
        protected override IEnumerable<string> IsNotSatisfiedBecause(IAggregateRoot obj)
        {
            if (obj.IsNew)
            {
                yield return $"Aggregate '{obj.Name}' with ID '{obj.GetIdentity()}' is new";
            }
        }
    }
}
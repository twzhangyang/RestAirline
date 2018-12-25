using System;
using System.Collections.Generic;
using FluentAssertions;
using RestAirline.Domain.Booking.Trip;
using RestAirline.TestsHelper;
using Xunit;

namespace RestAirline.Domain.Tests.Journeys
{
    public class JourneyValidationSpecificationTests
    {
        [Fact]
        public void WhenJourneyIsEmptyShouldReturnFalse()
        {
            //Arrange
            var spec = JourneyValidationSpecification.Create();
            var journeys = new List<Journey>();

            //Act
            var isSatisfy = spec.IsSatisfiedBy(journeys);
            var why = spec.WhyIsNotSatisfiedBy(journeys);

            //Assert
            isSatisfy.Should().BeFalse();
            why.Should().HaveCount(1);
        }
        
        [Fact]
        public void WhenDepartureDateLessThenNowShouldReturnFalse()
        {
            //Arrange
            var spec = JourneyValidationSpecification.Create();
            var journeys = new JourneysBuilder().BuildJourneys();
            journeys.ForEach(x=>x.DepartureDate=DateTime.Today.AddDays(-1));
            
            //Act
            var isSatisfy = spec.IsSatisfiedBy(journeys);
            var why = spec.WhyIsNotSatisfiedBy(journeys);
            
            //Assert
            isSatisfy.Should().BeFalse();
            why.Should().HaveCount(1);
        }
    }
}
using FluentAssertions;
using RestAirline.Api.Resources;
using Xunit;

namespace RestAirline.Api.Tests.Home
{
    public class HomeControllerTests : TestBase
    {
        [Fact]
        public async void ShouldGetHome()
        {
            //Act
            var home = await Get<RestAirlineHomeResource>("api/home");
            
            //Assert
            home.ResourceLinks.Should().NotBeNull();
        }
    }
}
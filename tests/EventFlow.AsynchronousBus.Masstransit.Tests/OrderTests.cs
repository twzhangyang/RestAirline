using System.Threading;
using FluentAssertions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EventFlow.AsynchronousBus.MassTransit.Tests
{
    public class OrderTests : TestBase
    {
        private readonly IBusControl _bus;

        public OrderTests()
        {
            _bus = ServiceProvider.GetService<IBusControl>();
        }
        
        public async void WhenPublishOrderCommittedEventShouldUpdateOrderState()
        {
            //Arrange
            var evt = new OrderCommittedEvent
            {
                Id = "1",
                Name = "order",
                Price = 10
            };
            
            //Act
            await HostedService.StartAsync(CancellationToken.None);
            await _bus.Publish(evt);
            await HostedService.StopAsync(CancellationToken.None);
            
            //Assert
            OrderState.Name.Should().Be(evt.Name);
            OrderState.Price.Should().Be(evt.Price);
        }
    }
}
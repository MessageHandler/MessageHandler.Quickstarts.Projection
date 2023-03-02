using FluentAssertions;
using MessageHandler.EventSourcing.Projections;
using MessageHandler.Quickstart.EventSourcing.Projection;
using MessageHandler.Quickstart.EventSourcing.Projection.API;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ComponentTests
{
    public class WhileQueryingPurchaseOrdersThroughAPI
    {
        [Fact]
        public void GivenRegistry_WhenCallingPreRestored_ThenRegistryIsCalled()
        {
            var registryMock = new Mock<IPurchaseOrdersRegistry>();
            registryMock.Setup(registry => registry.All()).Returns(new List<PurchaseOrder>());

            var projectionsMock = new Mock<IRestoreProjections<PurchaseOrder>>();

            var controller = new QueryController(registryMock.Object, projectionsMock.Object);

            var actionResult = controller.PreRestored();

            registryMock.Verify(registry => registry.All(), Times.Once());

            actionResult.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GivenRegistry_WhenCallingOnTheFly_ThenProjectionIsCalled()
        {
            var registryMock = new Mock<IPurchaseOrdersRegistry>();            

            var projectionsMock = new Mock<IRestoreProjections<PurchaseOrder>>();
            projectionsMock.Setup(registry => registry.Restore("OrderBooking")).ReturnsAsync(new Dictionary<string, PurchaseOrder>());

            var controller = new QueryController(registryMock.Object, projectionsMock.Object);

            var actionResult = await controller.OnTheFly();

            projectionsMock.Verify(projections => projections.Restore("OrderBooking"), Times.Once());

            actionResult.Should().BeOfType<OkObjectResult>();
        }
    }
}

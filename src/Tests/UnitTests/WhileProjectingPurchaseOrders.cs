using MessageHandler.EventSourcing.Testing;
using MessageHandler.Quickstart.Contract;
using MessageHandler.Quickstart.EventSourcing.Projection;
using Xunit;

namespace OrderBooking.UnitTests
{
    public class WhileProjectingPurchaseOrders
    {
        [Fact]
        public void GivenPurchaseOrderBooked_WhenPurchaseOrder_ThenBookingShouldHaveStatusPending()
        {
            // given
            var history = new OrderBookingHistoryBuilder()
                               .WellknownBooking("91d6950e-2ddf-4e98-a97c-fe5f434c13f0")
                               .Build();

            var purchaseOrder = new PurchaseOrder();

            // when
            var invoker = new TestProjectionInvoker<PurchaseOrder>(new ProjectInMemory());
            invoker.Invoke(purchaseOrder, history);

            // then
            Assert.Equal("Pending", purchaseOrder.Status);
        }

        [Fact]
        public void GivenPurchaseOrderBooked_WhenPurchaseOrder_ThenBookingShouldHaveOrderLines()
        {
            // given
            var history = new OrderBookingHistoryBuilder()
                               .WellknownBooking("91d6950e-2ddf-4e98-a97c-fe5f434c13f0")
                               .Build();

            var purchaseOrder = new PurchaseOrder();

            // when
            var invoker = new TestProjectionInvoker<PurchaseOrder>(new ProjectInMemory());
            invoker.Invoke(purchaseOrder, history);

            // then
            Assert.Collection(purchaseOrder.OrderLines,
                              line => Assert.Equal("7ccb042b-2fec-407d-963a-28dbe40bee6b", line.OrderLineId),
                              line => Assert.Equal("3da450d0-3215-4248-b07c-a8c22855d337", line.OrderLineId));
        }
    }
}

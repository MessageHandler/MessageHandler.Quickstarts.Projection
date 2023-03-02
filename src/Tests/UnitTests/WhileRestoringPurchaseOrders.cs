using MessageHandler.EventSourcing.Testing;
using MessageHandler.Quickstart.Contract;
using MessageHandler.Quickstart.EventSourcing.Projection;
using System.Threading.Tasks;
using Xunit;

namespace OrderBooking.UnitTests
{
    public class WhileRestoringPurchaseOrders
    {
        [Fact]
        public async Task GivenMultipleStreams_WhenRestoring_ThenMultipleInstancesShouldBeProjected()
        {
            // given
            var history = new OrderBookingHistoryBuilder()
                               .WellknownBooking("91d6950e-2ddf-4e98-a97c-fe5f434c13f0")
                               .WellknownBooking("c1a8b4a4-fac2-4ad6-a46a-243212f1363c")
                               .Build();

            // when
            var restorer = new TestProjectionRestorer<PurchaseOrder>(new ProjectInMemory());
            var purchaseOrders = await restorer.Restore(history);

            // then
            Assert.Equal("Pending", purchaseOrders["91d6950e-2ddf-4e98-a97c-fe5f434c13f0"].Status);
            Assert.Equal("Confirmed", purchaseOrders["c1a8b4a4-fac2-4ad6-a46a-243212f1363c"].Status);
        }
    }
}

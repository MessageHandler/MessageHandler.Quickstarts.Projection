using MessageHandler.EventSourcing.Projections;
using Microsoft.Extensions.Hosting;

namespace MessageHandler.Quickstart.EventSourcing.Projection
{
    public class RestoreOnStartup : IHostedService
    {
        private readonly PurchaseOrdersRegistry _registry;
        private readonly IRestoreProjections<PurchaseOrder> _projection;

        public RestoreOnStartup(IRestoreProjections<PurchaseOrder> projection, PurchaseOrdersRegistry registry)
        {
            _registry = registry;
            _projection = projection;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var restored = await _projection.Restore("OrderBooking", id => new PurchaseOrder() { BookingId = id });
            foreach(var order in restored.Values)
            {
                _registry.Index(order);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

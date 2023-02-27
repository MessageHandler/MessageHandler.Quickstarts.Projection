using MessageHandler.EventSourcing.Projections;
using Microsoft.Extensions.Hosting;

namespace MessageHandler.Samples.EventSourcing.Projection
{
    public class RestoreOnStartup : IHostedService
    {
        private readonly PurchaseOrdersRegistry _registry;
        private readonly IRestoreProjections<PurchaseOrdersRegistry> _projection;

        public RestoreOnStartup(IRestoreProjections<PurchaseOrdersRegistry> projection, PurchaseOrdersRegistry registry)
        {
            _registry = registry;
            _projection = projection;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _projection.Restore("OrderBooking", id => _registry);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

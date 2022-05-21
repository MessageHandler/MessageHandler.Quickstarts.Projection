using MessageHandler.EventSourcing.ProjectionRestoration;
using Microsoft.Extensions.Hosting;

namespace MessageHandler.Samples.EventSourcing.Projection
{
    public class RestoreOnStartup : IHostedService
    {
        private readonly PurchaseOrdersRegistry _registry;
        private readonly IExecuteProjections _projection;

        public RestoreOnStartup(IExecuteProjections projection, PurchaseOrdersRegistry registry)
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

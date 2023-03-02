using MessageHandler.EventSourcing.Projections;
using Microsoft.AspNetCore.Mvc;

namespace MessageHandler.Quickstart.EventSourcing.Projection.API
{
    [ApiController]
    [Route("api")]
    public class QueryController : ControllerBase
    {
        private readonly IPurchaseOrdersRegistry _registry;
        private readonly IRestoreProjections<PurchaseOrder> _projections;

        public QueryController(IPurchaseOrdersRegistry registry, IRestoreProjections<PurchaseOrder> projections)
        {
            _registry = registry;
            _projections = projections;
        }

        [HttpGet("prerestored")]
        //[Authorize]
        public IActionResult PreRestored()
        {
            return Ok(_registry.All());
        }

        [HttpGet("onthefly")]
        //[Authorize]
        public async Task<IActionResult> OnTheFly()
        {
            var indexed = await _projections.Restore("OrderBooking");

            return Ok(indexed.Values);
        }
    }
}
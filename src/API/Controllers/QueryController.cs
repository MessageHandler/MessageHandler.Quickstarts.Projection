using Microsoft.AspNetCore.Mvc;

namespace MessageHandler.Quickstart.EventSourcing.Projection.API
{
    [ApiController]
    [Route("api")]
    public class QueryController : ControllerBase
    {
        private readonly PurchaseOrdersRegistry _registry;

        public QueryController(PurchaseOrdersRegistry registry)
        {
            _registry = registry;
        }

        [HttpGet()]
        //[Authorize]
        public async Task<IActionResult> All()
        {
            return Ok(_registry.All());
        }
    }
}
using MessageHandler.EventSourcing.Projections;
using MessageHandler.Quickstart.Contract;

namespace MessageHandler.Samples.EventSourcing.Projection
{
    public class ProjectInMemory:
        IProjection<PurchaseOrdersRegistry, PurchaseOrderBooked>,
        IProjection<PurchaseOrdersRegistry, BookingCancelled>,
        IProjection<PurchaseOrdersRegistry, BookingConfirmed>
    {
        public void Project(PurchaseOrdersRegistry registry, PurchaseOrderBooked msg)
        {
            var order = registry.Get(msg.BookingId) ?? new PurchaseOrder() { BookingId = msg.BookingId };

            order.PurchaseOrderReference = msg.PurchaseOrderId;
            order.SellerReference = msg.SellerReference;
            order.BuyerReference = msg.BuyerReference;
            order.Status = "Pending";
            order.OrderLines = msg.OrderLines.Select(ol => new OrderLine
            {
                OrderLineId = ol.OrderLineId,
                Quantity = ol.Quantity,
                OrderedItem = new Item
                {
                    ItemId = ol.OrderedItem.ItemId,
                    CatalogId = ol.OrderedItem.CatalogId,
                    CollectionId = ol.OrderedItem.CollectionId,
                    Name = ol.OrderedItem.Name,
                    Price = new Price()
                    {
                        Currency = ol.OrderedItem.Price.Currency,
                        Value = ol.OrderedItem.Price.Value
                    }
                }
            }).ToList();
           

            registry.Index(order);
        }

        public void Project(PurchaseOrdersRegistry registry, BookingCancelled msg)
        {
            var order = registry.Get(msg.BookingId) ?? new PurchaseOrder() { BookingId = msg.BookingId };

            order.Status = "Cancelled";

            registry.Index(order);
        }

        public void Project(PurchaseOrdersRegistry registry, BookingConfirmed msg)
        {
            var order = registry.Get(msg.BookingId) ?? new PurchaseOrder() { BookingId = msg.BookingId };

            order.Status = "Confirmed";

            registry.Index(order);
        }
    }
}

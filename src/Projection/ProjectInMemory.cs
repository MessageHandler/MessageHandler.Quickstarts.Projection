using MessageHandler.EventSourcing.Projections;
using MessageHandler.Quickstart.Contract;

namespace MessageHandler.Quickstart.EventSourcing.Projection
{
    public class ProjectInMemory:
        IProjection<PurchaseOrder, PurchaseOrderBooked>,
        IProjection<PurchaseOrder, BookingCancelled>,
        IProjection<PurchaseOrder, BookingConfirmed>
    {
        public void Project(PurchaseOrder order, PurchaseOrderBooked msg)
        {
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
        }

        public void Project(PurchaseOrder order, BookingCancelled msg)
        {
            order.Status = "Cancelled";
        }

        public void Project(PurchaseOrder order, BookingConfirmed msg)
        {
            order.Status = "Confirmed";
        }
    }
}

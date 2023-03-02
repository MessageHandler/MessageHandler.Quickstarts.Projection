using System.Collections.Concurrent;

namespace MessageHandler.Quickstart.EventSourcing.Projection
{
    public class PurchaseOrdersRegistry : IPurchaseOrdersRegistry
    {
        private readonly ConcurrentDictionary<string, PurchaseOrder> _orders = new();
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, PurchaseOrder>> _ordersBySeller = new();
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, PurchaseOrder>> _ordersByBuyer = new();

        public void Index(PurchaseOrder order)
        {
            _orders.AddOrUpdate(order.BookingId, order, (id, r) => order);

            if (!string.IsNullOrEmpty(order.SellerReference))
            {
                var bySeller = _ordersBySeller.GetOrAdd(order.SellerReference, i => new ConcurrentDictionary<string, PurchaseOrder>());

                bySeller.AddOrUpdate(order.SellerReference, order, (id, r) => order);
            }

            if (!string.IsNullOrEmpty(order.SellerReference))
            {
                var byBuyer = _ordersByBuyer.GetOrAdd(order.BuyerReference, i => new ConcurrentDictionary<string, PurchaseOrder>());

                byBuyer.AddOrUpdate(order.BuyerReference, order, (id, r) => order);
            }
        }

        public PurchaseOrder Get(string bookingId)
        {
            _orders.TryGetValue(bookingId, out var order);
            return order;
        }

        public IList<PurchaseOrder> GetBySeller(string sellerReference)
        {
            if (_ordersBySeller.TryGetValue(sellerReference, out var orders))
            {
                return orders.Values.ToList();
            }
            else
            {
                return new List<PurchaseOrder>();
            }
        }

        public IList<PurchaseOrder> GetByBuyer(string buyerReference)
        {
            if (_ordersByBuyer.TryGetValue(buyerReference, out var orders))
            {
                return orders.Values.ToList();
            }
            else
            {
                return new List<PurchaseOrder>();
            }
        }

        public IList<PurchaseOrder> All()
        {
            return _orders.Values.Select(p => p).ToList();
        }
    }
}

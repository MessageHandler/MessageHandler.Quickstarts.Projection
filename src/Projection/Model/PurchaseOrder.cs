namespace MessageHandler.Quickstart.EventSourcing.Projection
{
    public class PurchaseOrder
    {
        public string BookingId { get; set; }

        public string PurchaseOrderReference { get; set; }

        public string SellerReference { get; set; }

        public string BuyerReference { get; set; }

        public string Status { get; set; }

        public IList<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}

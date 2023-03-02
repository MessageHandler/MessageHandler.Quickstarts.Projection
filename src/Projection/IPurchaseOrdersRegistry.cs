namespace MessageHandler.Quickstart.EventSourcing.Projection
{
    public interface IPurchaseOrdersRegistry
    {
        public void Index(PurchaseOrder order);

        public PurchaseOrder Get(string bookingId);

        public IList<PurchaseOrder> GetBySeller(string sellerReference);

        public IList<PurchaseOrder> GetByBuyer(string buyerReference);

        public IList<PurchaseOrder> All();
    }
}

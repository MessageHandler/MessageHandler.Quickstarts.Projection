namespace MessageHandler.Samples.EventSourcing.Projection
{
    public class OrderLine
    {
        public string OrderLineId { get; set; }
        public Item OrderedItem { get; set; }
        public double Quantity { get; set; }
    }
}
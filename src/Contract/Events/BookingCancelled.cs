using MessageHandler.EventSourcing.Contracts;

namespace MessageHandler.Samples.EventSourcing.Projection.Contract
{
    public class BookingCancelled : SourcedEvent
    {
        public Context Context { get; set; }

        public string BookingId { get; set; }
    }
}
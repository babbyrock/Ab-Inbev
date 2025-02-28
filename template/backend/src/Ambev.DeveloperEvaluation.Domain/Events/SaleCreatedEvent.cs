using Ambev.DeveloperEvaluation.Domain.Entities;
using Rebus.Messages;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent
    {
        public string Customer { get; set; }
        public string Branch { get; set; }
        public int CartId { get; set; }
    }
}

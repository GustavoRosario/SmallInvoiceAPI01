
using SmallInvoice.Domain.Constants;

namespace SmallInvoice.Domain.Entities
{
    public class BaseEntity
    {
        public Guid RefId { get; set; }
        public int ProcessModeId { get; set; } = ProcessMode.ONLINE;
        public bool Active { get; set; }
    }
}

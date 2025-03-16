using System.ComponentModel.DataAnnotations;

namespace SmallInvoice.Domain.Entities
{
    public class SvInvoiceState : BaseEntity
    {
        [Key]
        public int InvoiceStateId { get; set; }
        public int StateDescription { get; set; }
    }
}

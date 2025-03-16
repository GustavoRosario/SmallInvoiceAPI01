using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallInvoice.Domain.Entities
{
    public class SvCustomerKitchen : BaseEntity
    {
        [Key]
        public int CustomerKitchenId { get; set; }
        [MaxLength(50)]
        public string CustomerKitchenName { get; set; }
        public int CustomerId { get; set; }
        // public Guid CustomerId { get; set; }
        //[ForeignKey(nameof(CustomerId))]
        [ForeignKey("CustomerId")]
        public SvCustomer Customer { get; set; }
    }
}

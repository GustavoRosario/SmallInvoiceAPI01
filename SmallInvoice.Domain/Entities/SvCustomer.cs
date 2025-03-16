using System.ComponentModel.DataAnnotations;

namespace SmallInvoice.Domain.Entities
{
    public class SvCustomer : BaseEntity
    {
        [Key]
        public int CustomerId { get; set; }
        [MaxLength(50)]
        public string CustomerName { get; set; }
        [MaxLength(20)]
        public string Rnc { get; set; }
        [MaxLength(250)]
        public string AddressCustomer { get; set; }
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        [MaxLength(320)]
        public string? Email { get; set; }

    }
}

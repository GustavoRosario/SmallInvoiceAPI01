using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace SmallInvoice.Application.Dto
{
    public class UpdateCustomerDto
    {
        [Required]
        public Guid RefId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Rnc { get; set; }
        [Required]
        public string AddressCustomer { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}

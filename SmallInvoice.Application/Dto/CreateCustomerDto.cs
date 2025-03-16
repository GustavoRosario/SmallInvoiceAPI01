
namespace SmallInvoice.Application.Dto
{
    public class CreateCustomerDto
    {
        public string CustomerName { get; set; }
        public string Rnc { get; set; }
        public string AddressCustomer { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ProcessModeId { get; set; }
        public bool Active { get; set; }
    }
}


namespace SmallInvoice.Application.Dto
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public Guid? RefId { get; set; }
        public string CustomerName { get; set; }
        public string Rnc { get; set; }
        public string AddressCustomer { get; set; }
        public string PhoneNumber { get; set; }
        public int ProcessModeId { get; set; }
        public bool Active { get; set; }
    }
}

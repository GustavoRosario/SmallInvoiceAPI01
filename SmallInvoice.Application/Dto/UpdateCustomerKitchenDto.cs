
namespace SmallInvoice.Application.Dto
{
    public class UpdateCustomerKitchenDto
    {
        public Guid RefId { get; set; }
        public string CustomerKitchenName { get; set; }
        public int CustomerId { get; set; }
    }
}

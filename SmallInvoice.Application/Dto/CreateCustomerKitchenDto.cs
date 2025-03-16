
namespace SmallInvoice.Application.Dto
{
    public class CreateCustomerKitchenDto
    {
        public string CustomerKitchenName { get; set; }
        //public Guid CustomerId { get; set; }
        public int CustomerId { get; set; }
        public int ProcessModeId { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace SmallInvoice.Application.Dto
{
    public class CustomerKitchenDto
    {
        public int CustomerKitchenId { get; set; }
        public Guid RefId { get; set; } = Guid.Empty;
        public string CustomerKitchenName { get; set; }
        //public int ProcessModeId { get; set; }
        public bool Active { get; set; }
    }
}

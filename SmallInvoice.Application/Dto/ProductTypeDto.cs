
namespace SmallInvoice.Application.Dto
{
    public class ProductTypeDto
    {
        public int ProductTypeId { get; set; }
        public Guid? RefId { get; set; } = default(Guid?);
        public string? ProductTypeName { get; set; }
        public bool Active { get; set; }
    }
}

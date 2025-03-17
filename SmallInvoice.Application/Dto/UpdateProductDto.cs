
namespace SmallInvoice.Application.Dto
{
    public class UpdateProductDto
    {
        public Guid? RefId { get; set; } = default(Guid?);
        public int ProductTypeId { get; set; }
        public string? ProductName { get; set; }
    }
}


namespace SmallInvoice.Application.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public Guid? RefId { get; set; } = default(Guid?);
        public int ProductTypeId { get; set; }
        public string? ProductName { get; set; }
        //public decimal Price { get; set; }
        public int ProcessModeId { get; set; }
        public bool Active { get; set; }
    }
}


namespace SmallInvoice.Application.Dto
{
    public class PriceListDto
    {
        public int PriceListId { get; set; }
        public Guid? RefId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
    }
}

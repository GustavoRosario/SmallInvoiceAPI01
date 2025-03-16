
namespace SmallInvoice.Application.Dto
{
    public class CreatePriceListDto
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public decimal Price { get; set; } = 0.00M;
        public int ProcessModeId { get; set; }
    }
}

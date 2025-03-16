using System.ComponentModel.DataAnnotations;

namespace SmallInvoice.Domain.Entities
{
    public class SvProduct : BaseEntity
    {
        [Key]
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductName { get; set; }
        //public decimal Price { get; set; } = 0.0M;
    }
}

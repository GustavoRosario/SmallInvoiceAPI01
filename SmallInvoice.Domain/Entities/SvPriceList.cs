using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallInvoice.Domain.Entities
{
    public class SvPriceList : BaseEntity
    {
        [Key]
        public int PriceListId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }

        //[ForeignKey(nameof(ProductId))]
        //public ICollection<SvProduct> Product { get; set; } = new List<SvProduct>();
        public string ProductCode { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal Price { get; set; }
    }
}

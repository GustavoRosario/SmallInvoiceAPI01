using System.ComponentModel.DataAnnotations;

namespace SmallInvoice.Domain.Entities
{
    public class SvProductType : BaseEntity
    {
        [Key]
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
    }
}

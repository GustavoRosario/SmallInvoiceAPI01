using SmallInvoice.Infrastructure.Entity.Data;

namespace SmallInvoice.Infrastructure.Adapters.Repository
{
    public class RepositoryBase
    {
        protected SmallInvoice025DbContext Context { get; set; }
        protected int AffectedRecords { get; set; } = 0;
    }
}

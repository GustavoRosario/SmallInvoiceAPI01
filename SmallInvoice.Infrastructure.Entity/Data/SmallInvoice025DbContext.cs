using Microsoft.EntityFrameworkCore;
using SmallInvoice.Domain.Entities;

namespace SmallInvoice.Infrastructure.Entity.Data
{
    public class SmallInvoice025DbContext : DbContext
    {
        public SmallInvoice025DbContext(DbContextOptions<SmallInvoice025DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }

        public DbSet<SvProductType> SvProductType { get; set; }
        public DbSet<SvProduct> SvProduct { get; set; }
        public DbSet<SvCustomer> SvCustomer { get; set; }
        public DbSet<SvCustomerKitchen> SvCustomerKitchen { get; set; }
        public DbSet<SvProcessMode> SvProcessMode { get; set; }
        public DbSet<SvPriceList> SvPriceList { get; set; }
        public DbSet<SvInvoiceState> SvInvoiceState { get; set; }

    }
}

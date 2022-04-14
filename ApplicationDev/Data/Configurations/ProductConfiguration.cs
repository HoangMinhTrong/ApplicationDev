using ApplicationDev.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationDev.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(x => x.Store)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.StoreId);
            builder.HasOne(x => x.ProductCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId);
            builder.HasOne(x => x.ProductDiscount)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.DiscountId);
            builder.HasOne(x => x.ProductInventory)
                .WithOne(x => x.Product)
                .HasForeignKey<Product>(x => x.InventoryId);

        }
    }
}
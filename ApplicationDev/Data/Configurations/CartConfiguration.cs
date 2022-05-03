using ApplicationDev.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationDev.Data.Configuration
{
    public class CartConfiguration: IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasOne(x => x.ApplicationUser)
                .WithOne(x => x.CartItem)
                .HasForeignKey<CartItem>(x => x.UserId)
                .IsRequired(false);
        }
    }
}
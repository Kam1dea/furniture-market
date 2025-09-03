using Furniture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Furniture.Infrastructure.Persistence.Configurations;

public class ProductConfiguration: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(2000);
        builder.Property(p => p.Category).IsRequired();
        builder.Property(p => p.Color).HasMaxLength(30);
        builder.Property(p => p.Material).HasMaxLength(50);
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Width).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Height).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Depth).HasColumnType("decimal(18,2)");
        
        builder.HasOne(p=>p.WorkerProfile)
            .WithMany(wp => wp.Products)
            .HasForeignKey(p => p.WorkerProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
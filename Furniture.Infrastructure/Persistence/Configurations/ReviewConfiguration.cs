using Furniture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Furniture.Infrastructure.Persistence.Configurations;

public class ReviewConfiguration:  IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Rating).IsRequired().HasDefaultValue(5);
        builder.Property(r => r.Tittle).HasMaxLength(50);
        builder.Property(r => r.Content).HasMaxLength(500).IsRequired();
        
        builder.ToTable((table => table.HasCheckConstraint(
            name: "Check_Review_Rating",
            sql: "\"Rating\" BETWEEN 1 AND 5")));

        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);
        
        builder.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
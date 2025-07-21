using Furniture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Furniture.Infrastructure.Persistence.Configurations;

public class ReviewImageConfiguration:  IEntityTypeConfiguration<ReviewImage>
{
    public void Configure(EntityTypeBuilder<ReviewImage> builder)
    {
        builder.HasKey(ri => ri.Id);
        
        builder.Property(ri => ri.Url).HasMaxLength(500).IsRequired();

        builder.HasOne(ri => ri.Review)
            .WithMany(r => r.ReviewImages)
            .HasForeignKey(ri => ri.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
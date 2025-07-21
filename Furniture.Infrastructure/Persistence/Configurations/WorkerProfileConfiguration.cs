using Furniture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Furniture.Infrastructure.Persistence.Configurations;

public class WorkerProfileConfiguration:  IEntityTypeConfiguration<WorkerProfile>
{
    public void Configure(EntityTypeBuilder<WorkerProfile> builder)
    {
        builder.HasKey(wp => wp.Id);

        builder.Property(wp => wp.Description).HasMaxLength(500);
        builder.Property(wp => wp.Location).HasMaxLength(150);
        builder.Property(wp => wp.Rating).HasPrecision(3, 2).HasDefaultValue(0m);
        
        builder.HasOne(wp => wp.Worker)
            .WithOne(w => w.WorkerProfile)
            .HasForeignKey<WorkerProfile>(wp => wp.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(wp => wp.Products)
            .WithOne(p => p.WorkerProfile)
            .HasForeignKey(p => p.WorkerProfileId);
        
        builder.HasMany(wp => wp.Reviews)
            .WithOne(r => r.WorkerProfile)
            .HasForeignKey(r => r.WorkerProfileId);
    }
}
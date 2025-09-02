using Furniture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Furniture.Infrastructure.Persistence.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
        
        builder.HasIndex(u => new { u.FirstName, u.LastName })
            .HasDatabaseName("IX_Users_FirstName_LastName");
        
        builder.HasOne(u => u.WorkerProfile)
            .WithOne(wp => wp.Worker)
            .HasForeignKey<WorkerProfile>(wp => wp.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
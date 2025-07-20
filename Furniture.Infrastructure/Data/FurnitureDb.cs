using Furniture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Furniture.Infrastructure.Data;

public class FurnitureDb: IdentityDbContext<IdentityUser>
{
    public FurnitureDb(DbContextOptions<FurnitureDb>  options): base(options) {}
    
    public DbSet<Product> Products { get; set; }
}
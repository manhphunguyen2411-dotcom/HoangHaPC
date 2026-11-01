using HoangHaPC.Models;
using Microsoft.EntityFrameworkCore;

namespace HoangHaPC.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Order>().HasIndex(o => o.Code).IsUnique();
        b.Entity<Order>().Property(o => o.Total).HasPrecision(18, 0);
        b.Entity<OrderItem>().Property(i => i.UnitPrice).HasPrecision(18, 0);
        b.Entity<Order>().HasMany(o => o.Items).WithOne().HasForeignKey(i => i.OrderId).OnDelete(DeleteBehavior.Cascade);
    }
}

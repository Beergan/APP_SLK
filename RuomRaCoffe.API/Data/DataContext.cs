using Microsoft.EntityFrameworkCore;
using RuomRaCoffe.API.Data.Entities;
using RuomRaCoffe.API.Data.Migrations;

namespace RuomRaCoffe.API.Data;

public class DataContext :DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ShiftRecord> ShiftRecords { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Entities.User>().ToTable("Users");
        modelBuilder.Entity<Entities.UserRole>().ToTable("UserRoles");
        modelBuilder.Entity<Entities.Product>().ToTable("Products");
        modelBuilder.Entity<Entities.Order>().ToTable("Orders");
        modelBuilder.Entity<Entities.OrderItem>().ToTable("OrderItems");
        modelBuilder.Entity<Entities.ShiftRecord>().ToTable("ShiftRecords");
    }

}

using Microsoft.EntityFrameworkCore;
using store_accounting_system.core.Entities;

namespace store_accounting_system.data.DataAccess.Entities;

public class StoreDbContext :  DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Supply> Supplies { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var appDir = Path.Combine(root, "StoreAccountingSystem");
        Directory.CreateDirectory(appDir);         

        var dbPath = Path.Combine(appDir, "StoreDb.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
        modelBuilder.Entity<Product>().ToTable("Products");
        modelBuilder.Entity<Supply>().ToTable("Supplies");
    }
}
using Core.Constants;
using Core.Entities;
using Core.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;
public class AppDbContext : DbContext
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.MSSQL_Connection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>().Property(x=>x.IsDeleted).HasDefaultValue(false);
        modelBuilder.Entity<Customer>().Property(x=>x.IsDeleted).HasDefaultValue(false);
        modelBuilder.Entity<Seller>().Property(x=>x.IsDeleted).HasDefaultValue(false);
        modelBuilder.Entity<Category>().Property(x=>x.IsDeleted).HasDefaultValue(false);
        modelBuilder.Entity<Product>().Property(x=>x.IsDeleted).HasDefaultValue(false);
        modelBuilder.Entity<Order>().Property(x=>x.IsDeleted).HasDefaultValue(false);

        modelBuilder.Entity<Admin>().HasQueryFilter(x=>x.IsDeleted==false);
        modelBuilder.Entity<Customer>().HasQueryFilter(x=>x.IsDeleted==false);
        modelBuilder.Entity<Seller>().HasQueryFilter(x=>x.IsDeleted==false);
        modelBuilder.Entity<Category>().HasQueryFilter(x=>x.IsDeleted==false);
        modelBuilder.Entity<Product>().HasQueryFilter(x=>x.IsDeleted==false);
        modelBuilder.Entity<Order>().HasQueryFilter(x=>x.IsDeleted==false);

        modelBuilder.Entity<Admin>().Property(x=>x.ModifyAt).HasDefaultValue(null);
    }

}

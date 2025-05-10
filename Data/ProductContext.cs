namespace Selection.ProductService.Data;
using Microsoft.EntityFrameworkCore;
using Selection.ProductService.Models;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions options)
        : base(options)
    {

    }
    public DbSet<Product> Products { get; set; }

    // restricciones en columnas
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
        .Property(x => x.Price)
        .HasColumnType("decimal(18,2)");
    }
}

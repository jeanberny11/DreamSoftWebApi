using DreamSoftData.Entities.Inventory;
using DreamSoftData.Entities.Public;
using DreamSoftData.Entities.Sales;
using Microsoft.EntityFrameworkCore;

namespace DreamSoftData.Context;

public class DreamSoftDbContext(DbContextOptions<DreamSoftDbContext> options) : DbContext(options)
{
    public DbSet<Accounts> Accounts { get; set; } = null!;
    public DbSet<AccountTypes> AccountTypes { get; set; } = null!;
    public DbSet<Countries> Countries { get; set; } = null!;
    public DbSet<Genders> Genders { get; set; } = null!;
    public DbSet<MenuGroups> MenuGroups { get; set; } = null!;
    public DbSet<MenuOptions> MenuOptions { get; set; } = null!;
    public DbSet<Modules> Modules { get; set; } = null!;
    public DbSet<Municipalities> Municipalities { get; set; } = null!;
    public DbSet<Provinces> Provinces { get; set; } = null!;
    public DbSet<RoleOptions> RoleOptions { get; set; } = null!;
    public DbSet<Roles> Roles { get; set; } = null!;
    public DbSet<Users> Users { get; set; } = null!;
    public DbSet<RefreshTokens> RefreshTokens { get; set; } = null!;
    public DbSet<DefaultValSetups> DefaultValSetups { get; set; } = null!;
    public DbSet<Customers> Customers { get; set; } = null!;
    public DbSet<Prices> Prices { get; set; } = null!;
    public DbSet<MaritalStatus> MaritalStatus { get; set; } = null!;
    public DbSet<TaxType> TaxType { get; set; } = null!;
    public DbSet<Brands> Brands { get; set; } = null!;
    public DbSet<Categories> Categories { get; set; } = null!;
    public DbSet<Locations> Locations { get; set; } = null!;
    public DbSet<Models> Models { get; set; } = null!;
    public DbSet<Products> Products { get; set; } = null!;
    public DbSet<Suppliers> Suppliers { get; set; } = null!;
    public DbSet<Units> Units { get; set; } = null!;
    public DbSet<Warehouses> Warehouses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Here you can add Fluent API configurations if needed.
        // For example, if you want to explicitly configure table names or relationships:

        modelBuilder.Entity<Accounts>().ToTable("accounts");
        modelBuilder.Entity<AccountTypes>().ToTable("accounttypes");
        modelBuilder.Entity<Countries>().ToTable("countries");
        modelBuilder.Entity<Genders>().ToTable("genders");
        modelBuilder.Entity<MenuGroups>().ToTable("menugroups");
        modelBuilder.Entity<MenuOptions>().ToTable("menuoptions");
        modelBuilder.Entity<Modules>().ToTable("modules");
        modelBuilder.Entity<Municipalities>().ToTable("municipalities");
        modelBuilder.Entity<Provinces>().ToTable("provinces");
        modelBuilder.Entity<RoleOptions>().ToTable("roleoptions");
        modelBuilder.Entity<Roles>().ToTable("roles");
        modelBuilder.Entity<Users>().ToTable("users");
        modelBuilder.Entity<RefreshTokens>().ToTable("refreshtokens");
        modelBuilder.Entity<DefaultValSetups>().ToTable("defaultvaluesetup");
        modelBuilder.Entity<Customers>().ToTable("customers", "sales");
        modelBuilder.Entity<Prices>().ToTable("prices", "sales");
        modelBuilder.Entity<MaritalStatus>().ToTable("maritalstatus");
        modelBuilder.Entity<TaxType>().ToTable("taxtype");
        modelBuilder.Entity<Brands>().ToTable("brands", "inventory");
        modelBuilder.Entity<Categories>().ToTable("categories", "inventory");
        modelBuilder.Entity<Locations>().ToTable("locations", "inventory");
        modelBuilder.Entity<Models>().ToTable("models", "inventory");
        modelBuilder.Entity<Products>().ToTable("products", "inventory");
        modelBuilder.Entity<Suppliers>().ToTable("suppliers", "inventory");
        modelBuilder.Entity<Units>().ToTable("units", "inventory");
        modelBuilder.Entity<Warehouses>().ToTable("warehouses", "inventory");

        // Add any additional configuration or override defaults here
    }
}
using DreamSoftData.Entities.Public;
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

        // Add any additional configuration or override defaults here
    }
}
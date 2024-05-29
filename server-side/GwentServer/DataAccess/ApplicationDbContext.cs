using GwentServer.Models;
using Microsoft.EntityFrameworkCore;

namespace GwentServer.DataAccess;

public sealed class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Account> Accounts => Set<Account>();
    
    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;

        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(_configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(_configuration.GetConnectionString("DefaultConnection")));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Account>();

        entity.HasKey(a => a.Id);

        entity.Property(a => a.Login).IsRequired();
        entity.Property(a => a.Email).IsRequired();
        entity.Property(a => a.Password).IsRequired();
        
        base.OnModelCreating(modelBuilder);
    }
}

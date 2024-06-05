using Core.Models;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DbContexts;

public sealed class AccountDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<AccountEntity> Accounts => Set<AccountEntity>();
    
    public AccountDbContext(IConfiguration configuration)
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
        var entity = modelBuilder.Entity<AccountEntity>();

        entity.HasKey(a => a.Id);

        entity.Property(a => a.Login).HasMaxLength(Account.MAX_LENGTH_LOGIN).IsRequired();
        entity.Property(a => a.Email).IsRequired();
        entity.Property(a => a.Password).IsRequired();
        
        base.OnModelCreating(modelBuilder);
    }
}

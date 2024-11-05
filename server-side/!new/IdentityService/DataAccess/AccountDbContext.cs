using IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DataAccess;

public class AccountDbContext : DbContext
{
    public DbSet<AccountEntity> Accounts { get; set; }

    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<AccountEntity>();

        entity.HasKey(x => x.Id);
        entity.Property(x => x.Decks).HasColumnType("jsonb");
    }
}
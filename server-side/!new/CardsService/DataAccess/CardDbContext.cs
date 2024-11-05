using CardsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CardsService.DataAccess;

public class CardDbContext : DbContext
{
    public DbSet<CardEntity> Cards { get; set; }

    public CardDbContext(DbContextOptions<CardDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<CardEntity>();

        entity.HasKey(x => x.Id);
    }
}
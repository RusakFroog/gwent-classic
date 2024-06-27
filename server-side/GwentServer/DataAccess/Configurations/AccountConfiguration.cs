using Core.Enums.Game;
using Core.Models;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DataAccess.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AccountEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Login).HasMaxLength(Account.MAX_LENGTH_LOGIN).IsRequired();
        builder.Property(a => a.Name).HasMaxLength(Account.MAX_LENGTH_NAME).IsRequired();
        builder.Property(a => a.Email).IsRequired();
        builder.Property(a => a.Password).IsRequired();
        builder.Property(a => a.Decks)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<Fraction, List<int>>>(v));
    }
}
using Core.Enums.Game;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<CardEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CardEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Fraction)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (Fraction)Enum.Parse(typeof(Fraction), v));
        builder.Property(a => a.Name).IsRequired();
        builder.Property(a => a.Strength).IsRequired();
        builder.Property(a => a.HornBoost).IsRequired();
        builder.Property(a => a.CanBeTaken).IsRequired();
        builder.Property(a => a.WeatherImmunity).IsRequired();
        builder.Property(a => a.FieldLines);
    }
}
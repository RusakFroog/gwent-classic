using Core.Enums.Game;

namespace Core.Entities.Game.Cards;

public class Card
{
    public int Id { get; private set; }
    public sbyte Strength { get; private set; }
    public string Name { get; private set; }
    public Fraction Fraction { get; private set; }
    public IEnumerable<FieldLine> Fields { get; private set; }

    public virtual bool CanBeTaken { get; private set; } = true;
    public virtual bool HornBoost { get; private set; } = true;
    public virtual bool WeatherImmunity { get; private set; } = false;

    protected Card(string name, sbyte strength, Fraction fraction, IEnumerable<FieldLine> fields)
    {
        Name = name;
        Strength = strength;
        Fraction = fraction;
        Fields = fields;
    }

    private Card(int id, string name, sbyte strength, Fraction fraction, IEnumerable<FieldLine> fields, bool canBeTaken, bool hornBoost, bool weatherImmunity)
    {
        Id = id;
        Name = name;
        Strength = strength;
        Fraction = fraction;
        Fields = fields;

        CanBeTaken = canBeTaken;
        HornBoost = hornBoost;
        WeatherImmunity = weatherImmunity;
    }

    public static Card CreateCard(int id, string name, sbyte strength, Fraction fraction, IEnumerable<FieldLine> fields, bool canBeTaken, bool hornBoost, bool weatherImmunity)
    {
        return new Card(id, name, strength, fraction, fields, canBeTaken, hornBoost, weatherImmunity);
    }

    public static T CreateCard<T>(int id) where T : Card
    {
        var card = Activator.CreateInstance<T>();
        card.Id = id;

        return card;
    }
}

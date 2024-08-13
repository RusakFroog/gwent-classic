﻿using Core.Enums.Game;
using Core.Models.Game.Cards;
using Core.Models.Game.Decks;
using DataAccess.Entities;
using DataAccess.Intrefaces;
using DataAccess.Repositories;
namespace Application.Services;

public class DecksConvertorService
{
    private readonly CardsRepository _cardsRepository;

    public DecksConvertorService(IRepository<CardEntity> cardRepository)
    {
        _cardsRepository = (CardsRepository)cardRepository;
    }

    public List<Deck> ConvertIdsToDecks(Dictionary<Fraction, List<int>> decks)
    {
        List<Deck> result = [];

        foreach (var deck in decks)
        {
            List<Card> cards = [];

            Fraction fraction = deck.Key;
            var cardsId = deck.Value;

            foreach (int cardId in cardsId)
            {
                Card? card = _cardsRepository.GetCardById(cardId);

                ArgumentNullException.ThrowIfNull(card);

                cards.Add(card);
            }

            result.Add(new Deck(fraction, cards));
        }

        return result;
    }

    public Dictionary<Fraction, List<int>> ConvertDeckToIds(IEnumerable<Deck> decks)
    {
        Dictionary<Fraction, List<int>> result = [];

        foreach (var deck in decks)
        {
            if (result.ContainsKey(deck.Fraction))
                result[deck.Fraction].AddRange(deck.Cards.Select(x => x.Id));
            else
                result.Add(deck.Fraction, deck.Cards.Select(x => x.Id).ToList());
        }

        return result;
    }
}

import router from '../router/index.js';

const HTTP_SERVER = 'http://localhost:5187/api';

export const FRACTIONS = {
    Monsters: "Monsters",
    Nilfgaardian: "Nilfgaardian",
    NorthenRealms: "NorthenRealms",
    Scoiatael: "Scoiatael",
    Skellige: "Skellige",
};

const getFractionId = (fraction) => {
    switch (fraction) {
        case FRACTIONS.Monsters:
            return 1;
        case FRACTIONS.Nilfgaardian:
            return 2;
        case FRACTIONS.NorthenRealms:
            return 3;
        case FRACTIONS.Scoiatael:
            return 4;
        case FRACTIONS.Skellige:
            return 5;
        default:
            return 0;
    }
}

/**
 * @param {FRACTIONS} fraction
 */
export const getDeck = async (fraction) => { 
    const response = await fetch(`${HTTP_SERVER}/decks/get/${fraction}`, { 
        method: 'GET',
        credentials: 'include'
    });

    if (response.status === 400)
        return alert(await response.text());

    if (!response.ok)
        return router.push('/login');

    const cardsId = await response.json();

    const cards = [];

    for (const cardId of cardsId) {
        cards.push(await getCard(cardId));
    }

    return cards;
}

/**
 * @param {FRACTIONS} fraction
 * @param {number[]} cardsId
 */
export const updateDeck = async (fraction, cardsId) => {
    const response = await fetch(`${HTTP_SERVER}/decks/update`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            deckDTO: {
                fraction: getFractionId(fraction),
                cardIds: [cardsId]
            }
        }),
        credentials: 'include'
    });

    if (!response.ok)
        return router.push('/login');
}

const getCard = async (id) => {
    //release get cards from some CDN
    //return { img: "https://cdn.com/images/cards/card1.png" };
}
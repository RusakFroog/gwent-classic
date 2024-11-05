import router from '../router/index.js';
import { HTTP_SERVER, FRACTIONS } from './data/constants.js';
import { DECK_ERRORS } from './data/translates.js';
import Translation from './Translation.js';

const TranslationService = new Translation(DECK_ERRORS);

/**
 * @param {FRACTIONS} fraction
 */
export const getDeck = async (fraction) => { 
    const response = await fetch(`${HTTP_SERVER}/decks/get/${fraction}`, { 
        method: 'GET',
        credentials: 'include'
    });

    if (!response.ok) {
        const errorText = TranslationService.getTranslate(await response.text());
        
        return { error: true, errorMessage: errorText, result: null };
    }

    if (response.status === 400)
        return router.push('/login');

    const result = await response.json();
    
    return { error: false, errorMessage: null, result: result };
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
            fraction: fraction,
            deck: cardsId
        }),
        credentials: 'include'
    });

    if (!response.ok)
        return await response.json();
}
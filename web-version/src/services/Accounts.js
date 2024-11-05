import { HTTP_SERVER } from './data/constants.js'; 
import { ACCOUNT_ERRORS } from './data/translates.js';
import Translation from './Translation.js';

const TranslationService = new Translation(ACCOUNT_ERRORS);

export const createAccount = async (login, email, password) => {
    const response = await fetch(`${HTTP_SERVER}/accounts/create`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            login,
            email,
            password
        }),
        credentials: 'include'
    });

    return {
        response: response,
        error: response.ok ? null : TranslationService.getTranslate(await response.text())
    };
}

export const loginToAccount = async (login, password) => {
    const response = await fetch(`${HTTP_SERVER}/accounts/login`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            login,
            password
        }),
        credentials: 'include'
    });

    return {
        response: response,
        error: response.ok ? null : TranslationService.getTranslate(await response.text())
    };
}

export const logoutAccount = async () => {
    await fetch(`${HTTP_SERVER}/accounts/logout`, {
        method: 'POST',
        credentials: 'include'
    });
}

export const updateAccount = async (name) => {
    const response = await fetch(`${HTTP_SERVER}/accounts/update`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            name,
        }),
        credentials: 'include'
    });

    const error = await response.text();
    
    return {
        response: response,
        error: error
    };
}

export const loggedIn = async () => {
    const response = await fetch(`${HTTP_SERVER}/accounts/logged-in`, {
        method: 'GET',
        credentials: 'include'
    });

    if (!response.ok) {
        await logoutAccount();

        console.log("log out");
    }

    return response.ok;
}
const HTTP_SERVER = 'http://localhost:5187/api';

const registerErrors = {
    "LENGTH_LOGIN" : "Length of Login must 4-40 characters",
    "LENGTH_PASSWORD" : "Password must contains at least 6 characters",
    "ONLY_ALPHANUMERIC_LOGIN" : "Login must contains only letters from 'a-z' and '0-9'",
    "ONLY_ALPHANUMERIC_PASSWORD" : "Password must contains only letters 'a-Z' and '0-9'",
    "INVALID_EMAIL" : "Email is invalid",
    "EXIST_EMAIL" : "Account with current Email already registered",
    "EXIST_LOGIN" : "Account with current Login already registered"
};

const loginErrors = {
    "INVALID_DATA" : "Login or Password are invalid",
};

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
        })
    });

    return {
        response: response,
        error: response.ok ? null : registerErrors[await response.text()]
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
        })
    });

    return {
        response: response,
        error: response.ok ? null : loginErrors[await response.text()]
    };
}